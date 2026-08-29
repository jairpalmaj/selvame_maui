using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.StartAdventure;

public sealed partial class StartPageViewModel : BaseViewModel
{
    public StartPageViewModel(INavigationService navigationService,
        IAlertService alertService) : base(navigationService, alertService)
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