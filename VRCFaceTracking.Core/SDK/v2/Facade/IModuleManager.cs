namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleManager
{
    public void SubscribeToModuleListChanged(Action<IList<VrcftModuleV2>> onModuleListChanged); // TODO: Review this

    public bool IsStarted();
    public bool TrySetStart(bool start);

    public void SetAllowStart(bool allowStart);
    public bool GetAllowStart();

    public int GetPriority();
    public int SetPriority(int priority);

    public void Unload();
}
