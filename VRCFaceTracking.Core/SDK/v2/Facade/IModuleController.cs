using Microsoft.Extensions.Logging;

namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleController
{
    public ILoggerFactory GetLoggerFactory();
    public IModuleManager GetModuleManager();
    public IModuleUi GetUi();
}
