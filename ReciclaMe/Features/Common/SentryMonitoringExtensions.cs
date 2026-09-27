namespace ReciclaMe.Features.Common;

public static class SentryMonitoringExtensions
{
    public static MauiAppBuilder EnableSentry(this MauiAppBuilder builder)
    {
        builder.UseSentry(options =>
        {
            options.Dsn =
                "https://bd91a647449ddf89a876ece7e00678ca@o4512028797370368.ingest.us.sentry.io/4512028823388160";
#if Debug
                  options.Debug = true;      
#endif
            options.SendDefaultPii = false;
        });
        return builder;
    }
}