using System.Collections.ObjectModel;

namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes.Mixer;

public class MixerSettingsStorage
{
    public IReadOnlyDictionary<IParameterName, MixerParameterStateAndOrder> MixOrder
    {
        get;
        set;
    } = new Dictionary<IParameterName, MixerParameterStateAndOrder>();
}

public record MixerParameterStateAndOrder(bool IsEnabled, ReadOnlyCollection<Guid> Order);
