using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Menu;
using ReciclaMe.Services;

namespace ReciclaMe.Features.Alerts;

public sealed partial class MysteryObjectPageViewModel : BaseViewModel
{
    private ModelClassification _modelClassification;

    public MysteryObjectPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
    }

    public override async Task OnNavigatedTo()
    {
        AnalyticsService.Count(AnalyticsKeys.ObjectNotIdentify, 1, [
            new KeyValuePair<string, object>("Category", _modelClassification.CategoryClass.Category.ToString()),
            new KeyValuePair<string, object>("Accuracy", _modelClassification.Accuracy)
        ]);
        
        AnalyticsService.EmitDistribution(AnalyticsKeys.ObjectNotIdentifyAccuracy, _modelClassification.Accuracy, DistributionType.Percentage,[
            new KeyValuePair<string, object>("Category", _modelClassification.CategoryClass.Category.ToString()),
        ]);
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ModelClassification", out var modelClassification))
        {
            _modelClassification = (ModelClassification) modelClassification;
        }
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