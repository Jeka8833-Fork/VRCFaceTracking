using VRCFaceTracking.Core.SDK.v2.Core;
using VRCFaceTracking.Core.SDK.v2.Core.Pipeline;
using VRCFaceTracking.Core.SDK.v2.Util;

namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleParameterManager
{
    public void RegisterPipelineNode(IPipelineNode pipelineListener);
    public void UnregisterPipelineNode(IPipelineNode pipelineListener);

    public IPipelineNode[] GetModulePipelineNodes();

    public void SetParameterTimeout(IReadOnlyMutableTime timeout);
    public void SetValue<T>(ParameterName<T> parameter, T value);

    public void Flush();
    public void Flush(IReadOnlyMutableTime timestamp);
}
