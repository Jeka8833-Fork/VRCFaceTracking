namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleUi
{
    public void SetName(string name);
    public void SetDescription(string description);
    public void SetAllowStart(bool allowEnable);

    public void SetIcons(params Stream[] images);
    public bool SetIcons(params string[] imageAssetPaths);
}
