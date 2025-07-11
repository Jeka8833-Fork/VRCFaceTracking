using System.Collections.ObjectModel;

namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleManager
{
    public void SubscribeToModuleListChange(Action<ReadOnlyDictionary<Guid, VrcftModuleV2>> onModuleListChanged);

    public bool IsStarted();
    public bool TrySetStart(bool start);

    public void SetAllowStart(bool allowStart);
    public bool GetAllowStart();
}
