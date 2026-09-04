using ReciclaMe.Domain;
using ReciclaMe.Infrastructure;

namespace ReciclaMe.Features.Common;

public static class ServicesExtensions
{
    public static MauiAppBuilder AddServices(this MauiAppBuilder builder)
    {
        //navigation service
        builder.Services.AddSingleton<INavigationService, ShellNavigationService>();
        builder.Services.AddSingleton<IAlertService, ShellAlertService>();
        
        //Business services
        builder.Services.AddSingleton<ICategoryClassRepository, MockCategoryClassRepository>();
        builder.Services.AddSingleton<ICharacterRepository, MockCharacterRepository>();
        builder.Services.AddSingleton<IProfileRepository, SqliteProfileRepository>();
        builder.Services.AddSingleton<IClassificationModelService, MockClassificationModelService>();
        builder.Services.AddSingleton<IImageService, ImageService>();
        return builder;
    }
}