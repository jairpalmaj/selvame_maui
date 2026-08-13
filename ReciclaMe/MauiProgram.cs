using Microsoft.Extensions.Logging;

namespace ReciclaMe;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Fredoka-Bold.ttf", "FredokaOne");
                fonts.AddFont("Nunito-SemiBold", "Nunito");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}