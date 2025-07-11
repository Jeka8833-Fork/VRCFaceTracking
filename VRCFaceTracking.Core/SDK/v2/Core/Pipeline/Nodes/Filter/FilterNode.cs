using System.Collections.Concurrent;

namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes.Filter;

public class FilterNode(FilterSettingsStorage filterSettings) : IPipelineNode
{
    private const int DefaultFps = 20;

    private readonly ConcurrentDictionary<IParameterName, OneEuroFilter> _lastUpdateTime = new();

    public int Priority() => PipelinePriorityStage.FilterStage;

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters)
    {
        foreach (KeyValuePair<IParameterName, object> parameterEntry in parameters)
        {
            if (parameterEntry.Value is float value)
            {
                MutableOneEuroFilterParams filterParameters = filterSettings.FilterParams.GetValueOrDefault(
                    parameterEntry.Key, filterSettings.DefaultFilterParams);

                OneEuroFilter filter = _lastUpdateTime.GetOrAdd(parameterEntry.Key, _ => new OneEuroFilter(DefaultFps));

                parameters[parameterEntry.Key] =
                    (float)filter.Filter(filterParameters, value, header.Timestamp.GetNanoseconds());
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
            throw new InvalidOperationException($"FilterNode cannot have the same priority as another node ({other}).");
        }

        return result;
    }
}
