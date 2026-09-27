using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Achievements;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Menu;
using ReciclaMe.Infrastructure;
using ReciclaMe.Services;

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
    
    [ObservableProperty]
    private string _appVersion;
    
    public ProfilePageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository,
        IImageService imageService,
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        _imageService = imageService;
        _profileRepository = profileRepository;
        AppVersion = $"Versión {AppInfo.Current.VersionString} ({AppInfo.Current.BuildString})";
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
        AnalyticsService.Count(AnalyticsKeys.DeleteAccount, 1);
        await _profileRepository.DeleteAsync();
        _imageService.DeleteAll();
        var app = App.Current as App;
        app?.NavigateToChoosePage();
    }

    [RelayCommand]
    private async Task GoToPrivacyPolicy()
    {
        AnalyticsService.Count(AnalyticsKeys.PrivacyPolicySelection, 1);
        const string url = "https://jairpalma.com.mx/politica-de-privacidad-de-selvame/";
        await Launcher.Default.OpenAsync(url);
    }
    
    [RelayCommand]
    private async Task GoToFeedback()
    {
        AnalyticsService.Count(AnalyticsKeys.PrivacyPolicySelection, 1);
        const string url = "https://forms.gle/DNgraXCGKnEE3T13A";
        await Launcher.Default.OpenAsync(url);
    }
}