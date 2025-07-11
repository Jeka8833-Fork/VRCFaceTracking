namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes.Filter;

public class FilterSettingsStorage
{
    public MutableOneEuroFilterParams DefaultFilterParams
    {
        get;
        set;
    } = new();

    public IReadOnlyDictionary<IParameterName, MutableOneEuroFilterParams> FilterParams
    {
        get;
        set;
    } = new Dictionary<IParameterName, MutableOneEuroFilterParams>();
}
