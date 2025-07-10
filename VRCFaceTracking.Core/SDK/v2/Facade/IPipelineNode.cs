using VRCFaceTracking.Core.SDK.v2.Core;

namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IPipelineNode : IComparable<IPipelineNode>
{
    public int Priority();

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters);

    int IComparable<IPipelineNode>.CompareTo(IPipelineNode other)
    {
        return Priority().CompareTo(other.Priority());
    }
}
