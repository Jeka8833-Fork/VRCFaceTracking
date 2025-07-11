namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline;

public interface IPipelineNode : IComparable<IPipelineNode>
{
    public int Priority();

    public PacketHeader Process(PacketHeader header, IDictionary<IParameterName, object> parameters);

    int IComparable<IPipelineNode>.CompareTo(IPipelineNode other)
    {
        if (other == null) throw new ArgumentNullException(nameof(other));

        return Priority().CompareTo(other.Priority());
    }
}
