using ReciclaMe.Domain;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.StartAdventure;

public sealed partial class LoadingStartPageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    
    public LoadingStartPageViewModel(INavigationService navigationService, 
        IAlertService alertService,
        IProfileRepository profileRepository) : base(navigationService, alertService)
    {
        _profileRepository =  profileRepository;
    }

    public async override Task OnAppearing()
    {
        await Task.Delay(TimeSpan.FromSeconds(1.6));
        var profile = await _profileRepository.GetAsync();
        if (profile is null)
        {
            var app = App.Current as App;
            app?.NavigateToChoosePage();
        }
        else
        {
            var app = App.Current as App;
            app?.NavigateToMenu();
        }
    }
}