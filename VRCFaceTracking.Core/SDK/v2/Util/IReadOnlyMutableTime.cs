using System.Diagnostics;

namespace VRCFaceTracking.Core.SDK.v2.Util;

public interface IReadOnlyMutableTime
{
    protected static readonly double TicksPerMillisecond = Stopwatch.Frequency / 1000.0d;
    protected static readonly double TicksPerMicrosecond = Stopwatch.Frequency / 1_000_000.0d;
    protected static readonly double TicksPerNanosecond = Stopwatch.Frequency / 1_000_000_000.0d;

    public long GetTimeInTicks();

    /// <summary>
    /// Gets the time in days.
    /// </summary>
    public long GetDays()
    {
        return (long)(GetTimeInTicks() / (24.0d * 60.0d * 60.0d * Stopwatch.Frequency));
    }

    /// <summary>
    /// Gets the time in hours.
    /// </summary>
    public long GetHours()
    {
        return (long)(GetTimeInTicks() / (60.0d * 60.0d * Stopwatch.Frequency));
    }

    /// <summary>
    /// Gets the time in minutes.
    /// </summary>
    public long GetMinutes()
    {
        return (long)(GetTimeInTicks() / (60.0d * Stopwatch.Frequency));
    }

    /// <summary>
    /// Gets the time in seconds.
    /// </summary>
    public long GetSeconds()
    {
        return (long)(GetTimeInTicks() / (double)Stopwatch.Frequency);
    }

    /// <summary>
    /// Gets the time in milliseconds.
    /// </summary>
    public long GetMilliseconds()
    {
        return (long)(GetTimeInTicks() / TicksPerMillisecond);
    }

    /// <summary>
    /// Gets the time in microseconds.
    /// </summary>
    public long GetMicroseconds()
    {
        return (long)(GetTimeInTicks() / TicksPerMicrosecond);
    }

    /// <summary>
    /// Gets the time in nanoseconds.
    /// </summary>
    public long GetNanoseconds()
    {
        return (long)(GetTimeInTicks() / TicksPerNanosecond);
    }

    public bool IsExpired()
    {
        return Stopwatch.GetElapsedTime(GetTimeInTicks()).Ticks < 0;
    }
}
