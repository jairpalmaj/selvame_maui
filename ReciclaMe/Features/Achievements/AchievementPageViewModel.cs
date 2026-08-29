using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Menu;
using ReciclaMe.Features.Profile;

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
        IProfileRepository profileRepository) : base(navigationService, alertService)
    {
        _profileRepository = profileRepository;
        Badge1 = "badge_locked";
        Badge2 = "badge_locked";
        Badge3 = "badge_locked";
        Badge4 = "badge_locked";
    }

    public async override Task OnAppearing()
    {
        var character = await _profileRepository.GetCharacterAsync();
        CharacterImageSource = character.ImageSource;
        CharacterName = character.Name;
        Points = Profile.Points;
        var percentage = (UserProfile.Progress * Points) / 100;
        //convert from value 0 - 1
        Progress = percentage / 100f;
        ProgressLabel = $"{percentage}%";
        
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
        }
        
        if (points >= 40)
        {
            Badge2 = "badge_air";
        }
        
        if (points >= 60)
        {
            Badge3 = "badge_tree";
        }
        
        if (points >= 100)
        {
            Badge4 = "badge_monkey";
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