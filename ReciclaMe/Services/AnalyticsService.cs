using System.ComponentModel;

namespace ReciclaMe.Services;

public sealed class AnalyticsService : IAnalyticsService
{
    public void Count(string name, int value, KeyValuePair<string, object>[]? properties = null)
    {
        if (properties is null)
        {
            SentrySdk.Metrics.EmitCounter(name, value);
        }
        else
        {
            SentrySdk.Metrics.EmitCounter(name, value, properties);
        }
    }

    public void EmitDistribution(string name, float value, DistributionType type = DistributionType.Milliseconds, KeyValuePair<string, object> []? properties = null)
    {
        var sentryMetricType = GetSentryMetricType(type);

        if (properties is null)
        {
            SentrySdk.Metrics.EmitDistribution(name, value, sentryMetricType);
        }
        else
        {
            SentrySdk.Metrics.EmitDistribution(name, value, sentryMetricType,  properties);
        }
    }

    private MeasurementUnit GetSentryMetricType(DistributionType type)
    {
        switch (type)
        {
            case DistributionType.Milliseconds:
                return MeasurementUnit.Duration.Millisecond;
            case DistributionType.Percentage:
                return MeasurementUnit.Fraction.Percent;
            default:
                throw new InvalidEnumArgumentException(nameof(type), (int)type, typeof(DistributionType));
        }
    }
}