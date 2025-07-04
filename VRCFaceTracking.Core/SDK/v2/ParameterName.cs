namespace VRCFaceTracking.Core.SDK.v2;

public readonly struct ParameterName(string name)
{
    public string GetName() => name;

    public override string ToString() => name;
}
