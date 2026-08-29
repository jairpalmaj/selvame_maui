using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Alerts;
using ReciclaMe.Features.Classification;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Learn;

namespace ReciclaMe.Features.ExplorerLenses;

public sealed partial class LensesPageViewModel : BaseViewModel
{
    private readonly IClassificationModelService _classificationModelService;
    
    public LensesPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IClassificationModelService classificationModelService) : base(navigationService, alertService)
    {
        _classificationModelService = classificationModelService;
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