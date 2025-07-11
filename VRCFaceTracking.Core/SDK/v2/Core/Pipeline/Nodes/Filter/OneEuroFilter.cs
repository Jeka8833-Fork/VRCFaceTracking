namespace VRCFaceTracking.Core.SDK.v2.Core.Pipeline.Nodes.Filter;

// If you will be able to find library in Nuget - let me know, ty!
public class OneEuroFilter(int defaultFps)
{
    private readonly LowpassFilter _xFilt = new();
    private readonly LowpassFilter _dxFilt = new();

    private bool _firstTime = true;
    private long _prevTimestampNanos;

    public double Filter(MutableOneEuroFilterParams parameters, double x, long timestampNanos)
    {
        double deltaSeconds = _firstTime ? 1d / defaultFps : (timestampNanos - _prevTimestampNanos) * 1e-9d;
        if (deltaSeconds <= 0) deltaSeconds = 1d / defaultFps;
        _prevTimestampNanos = timestampNanos;

        double dx = _firstTime ? 0 : (x - _xFilt.Last()) * deltaSeconds;

        _firstTime = false;

        double edx = _dxFilt.Filter(dx, Alpha(deltaSeconds, parameters.DCutoff));
        double cutoff = parameters.MinCutoff + parameters.Beta * Math.Abs(edx);

        return _xFilt.Filter(x, Alpha(deltaSeconds, cutoff));
    }

    private static double Alpha(double rate, double cutoff)
    {
        double tau = 1.0 / (2 * Math.PI * cutoff);
        double te = 1.0 / rate;
        return 1.0 / (1.0 + tau / te);
    }
}

public class MutableOneEuroFilterParams
{
    public double MinCutoff
    {
        get;
        set;
    }

    public double Beta
    {
        get;
        set;
    }

    public double DCutoff
    {
        get;
        set;
    }
}

public class LowpassFilter
{
    private bool _firstTime = true;
    private double _hatXPrev;

    public double Filter(double x, double alpha)
    {
        if (_firstTime)
        {
            _firstTime = false;
            _hatXPrev = x;
        }
        else
        {
            _hatXPrev = alpha * x + (1 - alpha) * _hatXPrev;
        }

        return _hatXPrev;
    }

    public double Last()
    {
        return _hatXPrev;
    }
}
