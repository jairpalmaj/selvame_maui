namespace ReciclaMe.Services;

public interface ICrashReportService
{
    void CaptureError(Exception ex);
}