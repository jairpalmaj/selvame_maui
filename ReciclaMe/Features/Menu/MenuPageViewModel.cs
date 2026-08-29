using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Achievements;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Features.Profile;

namespace ReciclaMe.Features.Menu;

public sealed partial class MenuPageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private string? _characterImageSource;
    
    [ObservableProperty]
    private string? _characterName;
    
    [ObservableProperty]
    private int _points;
    
    public MenuPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository) : base(navigationService, alertService)
    {
        _profileRepository =  profileRepository;
    }

    public override async Task OnAppearing()
    {
        Profile = await _profileRepository.GetAsync();
        var character = await _profileRepository.GetCharacterAsync();
        CharacterImageSource = character.ImageSource;
        CharacterName = character.Name;
        Points = Profile.Points;
    }

    [RelayCommand]
    private async Task Play()
    {
        await NavigationService.NavigateAsync(nameof(LensesPageViewModel));
    }
    
    [RelayCommand]
    private async Task Learn()
    {
        await NavigationService.NavigateAsync(nameof(LearningLensesPageViewModel));
    }
    
    [RelayCommand]
    private async Task GoToAchievements()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(AchievementPageViewModel)}");
    }
    
    [RelayCommand]
    private async Task GoToProfile()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(ProfilePageViewModel)}");
    }
}