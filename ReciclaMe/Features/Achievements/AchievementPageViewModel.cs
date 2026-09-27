using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Menu;
using ReciclaMe.Features.Profile;
using ReciclaMe.Services;

namespace ReciclaMe.Features.Achievements;

public sealed partial class AchievementPageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private string? _characterImageSource;
    
    [ObservableProperty]
    private string? _characterName;
    
    [ObservableProperty]
    private int _points;
    
    [ObservableProperty] 
    private string _progressLabel;
    
    [ObservableProperty] 
    private float _progress;

    #region Badges
    [ObservableProperty] 
    private string _badge1;
    
    [ObservableProperty] 
    private string _badge2;
    
    [ObservableProperty] 
    private string _badge3;
    
    [ObservableProperty] 
    private string _badge4;
    #endregion
    
    public AchievementPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository,
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        _profileRepository = profileRepository;
        Badge1 = "badge_locked";
        Badge2 = "badge_locked";
        Badge3 = "badge_locked";
        Badge4 = "badge_locked";
    }

    public override async Task OnAppearing()
    {
        var character = await _profileRepository.GetCharacterAsync();
        CharacterImageSource = character.ImageSource;
        CharacterName = character.Name;
        Points = Profile.Points;
        var percentage = (UserProfile.Progress * Points) / 100;
        //convert from value 0 - 1
        Progress = percentage / 100f;
        ProgressLabel = $"{percentage}%";
        AnalyticsService.EmitDistribution(AnalyticsKeys.AppProgress, percentage, DistributionType.Percentage);
        //validate
        UnlockBadges(Points);
    }

    /// <summary>
    /// 20 puntos 1
    /// 40 puntos 2
    /// 60 puntos 3
    /// 100 puntos 4
    /// </summary>
    /// <param name="points"></param>
    private void UnlockBadges(int points)
    {
        if (points >= 20)
        {
            Badge1 = "badge_water";
            CheckAndSaveBadges(Badge1, 20);
        }
        
        if (points >= 40)
        {
            Badge2 = "badge_air";
            CheckAndSaveBadges(Badge2, 40);
        }
        
        if (points >= 60)
        {
            Badge3 = "badge_tree";
            CheckAndSaveBadges(Badge3, 60);
        }
        
        if (points >= 100)
        {
            Badge4 = "badge_monkey";
            CheckAndSaveBadges(Badge4, 100);
        }
    }

    /// <summary>
    /// This helps to check just once
    /// if the user got the badge
    /// previously
    /// </summary>
    private void CheckAndSaveBadges(string badgeName, int points)
    {
        if (!Preferences.Get(badgeName, false))
        {
            //we log once time user gets the badge
            Preferences.Set(badgeName, true);
            AnalyticsService.Count(AnalyticsKeys.BadgeEarned, 1, [
                new KeyValuePair<string, object>("Badge", badgeName),
                new KeyValuePair<string, object>("Points", points)
            ]);
        }
    }

    [RelayCommand]
    private async Task GoToHome()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(MenuPageViewModel)}");
    }
    
    [RelayCommand]
    private async Task GoToProfile()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(ProfilePageViewModel)}");
    }
}