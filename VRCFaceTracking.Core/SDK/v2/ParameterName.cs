namespace VRCFaceTracking.Core.SDK.v2;

public class ParameterName<T>(string name) : IParameterName
{
    public string GetName() => name;
    public Type GetParameterType() => typeof(T);

    public override string ToString() => name;
}
