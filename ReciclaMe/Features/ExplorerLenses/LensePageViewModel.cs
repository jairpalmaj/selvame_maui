using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Alerts;
using ReciclaMe.Features.Classification;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Learn;
using ReciclaMe.Infrastructure;
using ReciclaMe.Services;

namespace ReciclaMe.Features.ExplorerLenses;

public sealed partial class LensesPageViewModel : BaseViewModel
{
    private readonly IClassificationModelService _classificationModelService;
    private readonly IImageService _imageService;

    [ObservableProperty] 
    private bool _isEnabled;
    
    [ObservableProperty] 
    private bool _isRunning;
    
    public LensesPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IClassificationModelService classificationModelService,
        IImageService imageService, 
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        _imageService = imageService;
        _classificationModelService = classificationModelService;
        IsEnabled = true;
    }
    
    [RelayCommand]
    private async Task CapturePicture(Stream pictureStream)
    {
        IsEnabled = false;
        IsRunning = true;
        var filePath = _imageService.SavePicture(pictureStream);
        var modelClassification = await _classificationModelService.GetClassificationAsync(filePath);
        modelClassification.ImagePath = filePath;
        var parameters = new Dictionary<string, object>
        {
            { "ModelClassification", modelClassification }
        };
        
        if (modelClassification.Accuracy < AppPreferences.Accuracy)
        {
            await NavigationService.NavigateAsync(nameof(MysteryObjectPageViewModel), parameters);
        }
        else
        {
            await NavigationService.NavigateAsync(nameof(ClassificationPageViewModel), parameters);
        }

        IsEnabled = true;
        IsRunning = false;
    }
    
    protected override async Task Back()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}");
    }

    [RelayCommand]
    private async Task OpenExplanation()
    {
        AnalyticsService.Count(AnalyticsKeys.ExplanationSelection, 1);
        await NavigationService.NavigateAsync($"{nameof(ExplanationLensesPageViewModel)}");
    }

    [RelayCommand]
    private async Task Capture()
    {
        var modelClassification = await _classificationModelService.GetClassificationAsync();
        if (modelClassification.Accuracy > 0.61)
        {
            var parameters = new Dictionary<string, object>
            {
                { "ModelClassification", modelClassification }
            };
            await NavigationService.NavigateAsync(nameof(ClassificationPageViewModel), parameters);
        }
        else
        {
            await NavigationService.NavigateAsync(nameof(MysteryObjectPageViewModel));
        }
    }
    
    [RelayCommand]
    private async Task Learn()
    {
        await NavigationService.NavigateAsync(nameof(LearnPageViewModel));
    }
    
    [RelayCommand]
    private async Task Mystery()
    {
        await NavigationService.NavigateAsync(nameof(MysteryObjectPageViewModel));
    }
}