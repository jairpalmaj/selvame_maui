using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Core;
using Microsoft.Extensions.Logging;
using ReciclaMe.Features.Common;

namespace ReciclaMe;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiCommunityToolkitCore()
            .UseMauiCommunityToolkit()
            .UseMauiCommunityToolkitCamera()
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Fredoka-Bold.ttf", "FredokaOne");
                fonts.AddFont("Nunito-Bold.ttf", "Nunito");
                fonts.AddFont("MaterialSymbolsRounded_Filled-Bold.ttf", "GIcons");
            })
            .AddServices()
            .AddViewModels();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}