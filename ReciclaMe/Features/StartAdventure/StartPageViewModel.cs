using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Features.Common;
using ReciclaMe.Services;

namespace ReciclaMe.Features.StartAdventure;

public sealed partial class StartPageViewModel : BaseViewModel
{
    public StartPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
    }

    [RelayCommand]
    private void Start()
    {
        //await NavigationService.NavigateAsync($"///{nameof(MenuPageViewModel)}");
        var app = App.Current as App;
        app?.NavigateToMenu();
    }
}