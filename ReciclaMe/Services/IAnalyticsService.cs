namespace ReciclaMe.Services;

public interface IAnalyticsService
{
    void Count(string name, int value, KeyValuePair<string, object> []? properties = null);
    void EmitDistribution(string name, float value, DistributionType type = DistributionType.Milliseconds, KeyValuePair<string, object> []? properties = null);
}

public enum DistributionType : ushort
{
    None = 0,
    Percentage = 1,
    Milliseconds = 2
}