using System.Diagnostics;

namespace VRCFaceTracking.Core.SDK.v2.Util;

/// <summary>
/// Represents a time value based on Stopwatch ticks.
/// 1 tick = 1 tick of the system Stopwatch.
/// This class is mutable.
/// </summary>
public class MutableTime(long timeInTicks)
    : IReadOnlyMutableTime, IComparable<IReadOnlyMutableTime>, IEquatable<IReadOnlyMutableTime>
{
    private long _timeInTicks = timeInTicks;

    public long GetTimeInTicks() => _timeInTicks;
    public void SetTimeInTicks(long timeInTicks) => _timeInTicks = timeInTicks;

    /// <summary>
    /// Gets the current system time in Stopwatch ticks.
    /// </summary>
    public static MutableTime GetCurrentTime()
    {
        return new MutableTime(Stopwatch.GetTimestamp());
    }

    /// <summary>
    /// Creates a MutableTime from a value in days.
    /// </summary>
    public static MutableTime FromDays(long days)
    {
        return new MutableTime(days * 24L * 60L * 60L * Stopwatch.Frequency);
    }

    /// <summary>
    /// Creates a MutableTime from a value in hours.
    /// </summary>
    public static MutableTime FromHours(long hours)
    {
        return new MutableTime(hours * 60L * 60L * Stopwatch.Frequency);
    }

    /// <summary>
    /// Creates a MutableTime from a value in minutes.
    /// </summary>
    public static MutableTime FromMinutes(long minutes)
    {
        return new MutableTime(minutes * 60L * Stopwatch.Frequency);
    }

    /// <summary>
    /// Creates a MutableTime from a value in seconds.
    /// </summary>
    public static MutableTime FromSeconds(long seconds)
    {
        return new MutableTime(seconds * Stopwatch.Frequency);
    }

    /// <summary>
    /// Creates a MutableTime from a value in milliseconds.
    /// </summary>
    public static MutableTime FromMilliseconds(long milliseconds)
    {
        return new MutableTime((long)(milliseconds * IReadOnlyMutableTime.TicksPerMillisecond));
    }

    /// <summary>
    /// Creates a MutableTime from a value in microseconds.
    /// </summary>
    public static MutableTime FromMicroseconds(long microseconds)
    {
        return new MutableTime((long)(microseconds * IReadOnlyMutableTime.TicksPerMicrosecond));
    }

    /// <summary>
    /// Creates a MutableTime from a value in nanoseconds.
    /// </summary>
    public static MutableTime FromNanoseconds(long nanoseconds)
    {
        return new MutableTime((long)(nanoseconds * IReadOnlyMutableTime.TicksPerNanosecond));
    }

    public static MutableTime operator +(MutableTime a, MutableTime b) => new(a._timeInTicks + b._timeInTicks);
    public static MutableTime operator -(MutableTime a, MutableTime b) => new(a._timeInTicks - b._timeInTicks);

    public static bool operator <(MutableTime a, MutableTime b) => a._timeInTicks < b._timeInTicks;
    public static bool operator >(MutableTime a, MutableTime b) => a._timeInTicks > b._timeInTicks;
    public static bool operator <=(MutableTime a, MutableTime b) => a._timeInTicks <= b._timeInTicks;
    public static bool operator >=(MutableTime a, MutableTime b) => a._timeInTicks >= b._timeInTicks;

    public int CompareTo(IReadOnlyMutableTime other)
    {
        if (other is null) return 1;

        return GetTimeInTicks().CompareTo(other.GetTimeInTicks());
    }

    public bool Equals(IReadOnlyMutableTime other)
    {
        if (other is null) return false;

        return GetTimeInTicks() == other.GetTimeInTicks();
    }

    /// <summary>
    /// Returns the string representation of the time in ticks.
    /// </summary>
    public override string ToString() => $"{_timeInTicks} ticks";
}
