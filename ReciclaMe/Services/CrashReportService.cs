namespace ReciclaMe.Services;

public sealed class CrashReportService : ICrashReportService
{
    public void CaptureError(Exception ex)
    {
       SentrySdk.CaptureException(ex);
    }
}