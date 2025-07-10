#nullable enable
using VRCFaceTracking.Core.SDK.v2.Facade;

namespace VRCFaceTracking.Core.SDK.v2.Core;

public class ParameterName<T>(string name) : IParameterName, IEquatable<ParameterName<T>>
{
    public string GetName() => name;
    public Type GetParameterType() => typeof(T);

    public override string ToString() => name + " (" + typeof(T).Name + ")";

    public bool Equals(ParameterName<T>? other)
    {
        if (other is null) return false;
        if (ReferenceEquals(this, other)) return true;

        return name == other.GetName();
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;

        return Equals((ParameterName<T>)obj);
    }

    public override int GetHashCode()
    {
        return name.GetHashCode();
    }
}
