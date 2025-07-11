using System.Collections.Concurrent;
using VRCFaceTracking.Core.SDK.v2.Util;

namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes;

public class CachedNodeListener(
    int stage,
    int priority,
    CachedNodeListener.CachedListener listener,
    params IParameterName[] cachedParameters) : IPipelineNode
{
    private readonly ConcurrentDictionary<IParameterName, CachedValue> _parameterCache = new();

    public int Priority() => stage + priority;

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters)
    {
        foreach (var cacheEntry in _parameterCache)
        {
            if (cacheEntry.Value.ExpireTime.IsExpired())
            {
                _parameterCache.TryRemove(cacheEntry.Key, out _);
            }
        }


        PacketHeader newPacketHeader = listener(header, parameters, _parameterCache
            .ToDictionary(x => x.Key, x => x.Value.Value));

        foreach (IParameterName parameterName in cachedParameters)
        {
            if (parameters.TryGetValue(parameterName, out var value))
            {
                _parameterCache[parameterName] = new CachedValue(header.ExpireTime, value);
            }
        }

        return newPacketHeader;
    }

    // Be careful with this delegate, it is not thread-safe
    public delegate PacketHeader CachedListener(PacketHeader header, IDictionary<IParameterName, object> newParameters,
        IDictionary<IParameterName, object> cachedParameters);

    private record CachedValue(IReadOnlyMutableTime ExpireTime, object Value);
}
