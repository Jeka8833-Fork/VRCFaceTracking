using VRCFaceTracking.Core.SDK.v2.Core;

namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleParameterManager
{
    public void SetParameterTimeout(TimeSpan timeout);
    public void InvalidateAllParameterTimeouts();

    public void RegisterPipelineNode(IPipelineNode pipelineListener);
    public void UnregisterPipelineNode(IPipelineNode pipelineListener);

    public void SetValue<T>(ParameterName<T> parameter, T value);

    public void Flush();
    public void Flush(TimeSpan timestampNanos);
}
