namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleUi
{
    public void SetTitle(string name);
    public string GetTitle();

    public void SetStatus(string description);
    public string GetStatus();

    public void SetIcons(params Stream[] images);
    public bool SetIcons(params string[] imageAssetPaths);
}
