using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Menu;

namespace ReciclaMe.Features.Alerts;

public sealed partial class MysteryObjectPageViewModel : BaseViewModel
{
    public MysteryObjectPageViewModel(INavigationService navigationService,
        IAlertService alertService) : base(navigationService, alertService)
    {
    }
    
    [RelayCommand]
    private async Task GoToBase()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}");
    }
    
    [RelayCommand]
    private async Task Retry()
    {
        await NavigationService.GoBackAsync();
    }
}