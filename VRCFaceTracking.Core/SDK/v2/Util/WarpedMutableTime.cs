using System.Diagnostics;

namespace VRCFaceTracking.Core.SDK.v2.Util;

// 1 tick = 1 tick of stopwatch
public class WarpedMutableTime(long timeInTicks) : IReadOnlyWarpedMutableTime
{
    private long _timeInTicks = timeInTicks;

    public long GetTimeInTicks() => _timeInTicks;
    public void SetTimeInTicks(long timeInTicks) => _timeInTicks = timeInTicks;

    public static WarpedMutableTime GetCurrentTime()
    {
        return new WarpedMutableTime(Stopwatch.GetTimestamp());
    }

    /*public static WarpedMutableTime operator +(WarpedMutableTime time, TimeSpan span)
    {
        return new WarpedMutableTime(time._timeInTicks + ticks);
    }*/
}
