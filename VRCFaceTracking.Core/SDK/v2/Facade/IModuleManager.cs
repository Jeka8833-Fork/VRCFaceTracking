namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleManager
{
    public void SubscribeToModuleListChanged(IModuleEvents moduleEvents);

    public void TryStartModule();

    public void UnloadModule();
}
