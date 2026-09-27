using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Achievements;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Features.Profile;
using ReciclaMe.Services;

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
        IProfileRepository profileRepository, 
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
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
        AnalyticsService.Count(AnalyticsKeys.PlayFeature, 1);
        await CheckPermissionsAndNavigateAsync(nameof(LensesPageViewModel));
    }

    private async Task CheckPermissionsAndNavigateAsync(string path)
    {
        try
        {
            var cameraPermissionsRequest = await Permissions.RequestAsync<Permissions.Camera>();
            if (cameraPermissionsRequest == PermissionStatus.Granted ||
                cameraPermissionsRequest == PermissionStatus.Limited)
            {
                await NavigationService.NavigateAsync(path);
                return;
            }

            var isSetUpSelected = await AlertService.DisplayAlertAsync("¡La cámara se quedó dormida!",
                "Pídele ayuda a mamá, papá o a un adulto para despertarla en los ajustes del dispositivo y seguir jugando a clasificar.",
                "Configurar", "OK");
            if (isSetUpSelected)
            {
                AnalyticsService.Count(AnalyticsKeys.EnableCameraPermissions, 1);
                AppInfo.ShowSettingsUI();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    [RelayCommand]
    private async Task Learn()
    {
        AnalyticsService.Count(AnalyticsKeys.LearnFeature, 1);
        await CheckPermissionsAndNavigateAsync(nameof(LearningLensesPageViewModel));
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