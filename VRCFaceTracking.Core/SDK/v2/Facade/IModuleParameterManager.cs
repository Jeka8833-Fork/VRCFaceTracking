namespace VRCFaceTracking.Core.SDK.v2.Facade;

public interface IModuleParameterManager
{
    public void SetParameterTimeout(TimeSpan timeout);

    public void Subscribe(int pipelinePriority, Action<Dictionary<IParameterName, object>> callback,
        params IParameterName[] observableParameters);

    public void SetValue<T>(ParameterName<T> parameter, T value);
    public void ClearValue(params IParameterName[] parameters);

    public void Flush();
    public void Flush(long timestampNanos);
}
