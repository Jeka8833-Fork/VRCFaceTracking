namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes;

public class SimpleNodeListener(int stage, int priority, SimpleNodeListener.Listener listener) : IPipelineNode
{
    public int Priority() => stage + priority;

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters)
    {
        return listener(header, parameters);
    }

    // Be careful with this delegate, it is not thread-safe
    public delegate PacketHeader Listener(PacketHeader header, IDictionary<IParameterName, object> parameters);
}
