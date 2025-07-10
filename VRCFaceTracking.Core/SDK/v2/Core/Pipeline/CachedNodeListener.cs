using System.Collections.Concurrent;
using VRCFaceTracking.Core.SDK.v2.Facade;

namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline;

public class CachedNodeListener(
    int priority,
    CachedNodeListener.CachedListener listener,
    params IParameterName[] cachedParameters)
    : IPipelineNode
{
    private readonly ConcurrentDictionary<IParameterName, object> _parameterCache = new();

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters)
    {
        PacketHeader newPacketHeader = listener(header, parameters, _parameterCache);

        foreach (IParameterName parameterName in cachedParameters)
        {
            if (parameters.TryGetValue(parameterName, out var value))
            {
                _parameterCache[parameterName] = value;
            }
        }

        return newPacketHeader;
    }

    public int Priority() => priority;

    public delegate PacketHeader CachedListener(PacketHeader header, IDictionary<IParameterName, object> newParameters,
        IDictionary<IParameterName, object> cachedParameters);
}
