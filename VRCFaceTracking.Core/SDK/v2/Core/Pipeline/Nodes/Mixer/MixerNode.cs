using System.Collections.Concurrent;
using System.Diagnostics;

namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes.Mixer;

public class MixerNode(MixerSettingsStorage settingsStorage) : IPipelineNode
{
    private static readonly TimeSpan CleanInterval = TimeSpan.FromSeconds(10);


    private readonly ConcurrentDictionary<IParameterName, PacketHeader> _lastUpdateTime = new();
    private long _lastCleanTime = Stopwatch.GetTimestamp();

    public int Priority() => PipelinePriorityStage.MixerStage;

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters)
    {
        CleanOldParameters();

        foreach (var parameterEntry in parameters)
        {
            MixerParameterStateAndOrder stateAndOrder = settingsStorage.MixOrder.GetValueOrDefault(parameterEntry.Key);

            if (stateAndOrder != null && !stateAndOrder.IsEnabled)
            {
                parameters.Remove(parameterEntry.Key);

                continue;
            }

            PacketHeader updatedHeader = _lastUpdateTime.AddOrUpdate(parameterEntry.Key, header,
                (k, v) =>
                {
                    if (v.ModuleId == header.ModuleId || v.ExpireTime.IsExpired())
                    {
                        return header;
                    }

                    if (stateAndOrder != null)
                    {
                        int currentModuleIndex = stateAndOrder.Order.IndexOf(header.ModuleId); // O(N)
                        if (currentModuleIndex == -1) return v;

                        int previousModuleIndex = stateAndOrder.Order.IndexOf(v.ModuleId); // O(N)
                        if (previousModuleIndex == -1) return v;

                        if (currentModuleIndex < previousModuleIndex)
                        {
                            return header;
                        }
                    }

                    return v;
                });

            if (updatedHeader.ModuleId != header.ModuleId)
            {
                parameters.Remove(parameterEntry.Key);
            }
        }

        return header;
    }

    public int CompareTo(IPipelineNode other)
    {
        if (other is null) throw new ArgumentNullException(nameof(other));

        int result = Priority().CompareTo(other.Priority());
        if (result == 0)
        {
            throw new InvalidOperationException($"MixerNode cannot have the same priority as another node ({other}).");
        }

        return result;
    }

    private void CleanOldParameters()
    {
        if (Stopwatch.GetElapsedTime(_lastCleanTime) > CleanInterval)
        {
            _lastCleanTime = Stopwatch.GetTimestamp();

            foreach (var entry in _lastUpdateTime)
            {
                if (entry.Value.ExpireTime.IsExpired())
                {
                    _lastUpdateTime.TryRemove(entry.Key, out _);
                }
            }
        }
    }
}
