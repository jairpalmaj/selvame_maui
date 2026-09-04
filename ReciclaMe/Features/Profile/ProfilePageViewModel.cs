using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Achievements;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Menu;
using ReciclaMe.Infrastructure;

namespace ReciclaMe.Features.Profile;

public sealed partial class ProfilePageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    private readonly IImageService _imageService;

    [ObservableProperty]
    private string _characterName;
    
    [ObservableProperty]
    private string _characterImageSource;
    
    [ObservableProperty]
    private string _characterSkillIcon;
    
    [ObservableProperty]
    private string _characterSkillName;
    
    public ProfilePageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository,
        IImageService imageService) : base(navigationService, alertService)
    {
        _imageService = imageService;
        _profileRepository = profileRepository;
    }

    public override async Task OnAppearing()
    {
        var character = await _profileRepository.GetCharacterAsync();
        CharacterName = character.Name;
        CharacterImageSource = character.ImageSource;
        CharacterSkillIcon = character.SkillIcon;
        CharacterSkillName = character.Skill;
    }

    [RelayCommand]
    private async Task GoToHome()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(MenuPageViewModel)}");
    }
    
    [RelayCommand]
    private async Task GoToAchievements()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(AchievementPageViewModel)}");
    }
    
    [RelayCommand]
    private async Task Delete()
    {
        await _profileRepository.DeleteAsync();
        _imageService.DeleteAll();
        var app = App.Current as App;
        app?.NavigateToChoosePage();
    }

    [RelayCommand]
    private async Task GoToPrivacyPolicy()
    {
        const string url = "https://jairpalma.com.mx/politica-de-privacidad-de-selvame/";
        await Launcher.Default.OpenAsync(url);
    }
}