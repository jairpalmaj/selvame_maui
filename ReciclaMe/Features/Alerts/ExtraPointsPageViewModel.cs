using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Services;

namespace ReciclaMe.Features.Alerts;

public sealed partial class ExtraPointsPageViewModel : BaseViewModel
{
    private ModelClassification _modelClassification;

    [ObservableProperty]
    private string _imageSource;
    
    [ObservableProperty]
    private string _categoryName;
    
    [ObservableProperty]
    private string _notCategoryName;
    
    [ObservableProperty]
    private string _pointsLabel;
    
    [ObservableProperty]
    private string _pointsInformation;
    
    public ExtraPointsPageViewModel(INavigationService navigationService, 
        IAlertService alertService,         
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        
    }
    
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ModelClassification", out var modelClassification))
        {
            _modelClassification = (ModelClassification) modelClassification;
            ImageSource =  _modelClassification.ImagePath;
            CategoryName = $"¡Es {_modelClassification.CategoryClass.Title}!";
            PointsLabel = $"{GamePoints.Success}+ Puntos de Selva";
            PointsInformation = $"Has ganado {GamePoints.ExtraPoint}+ Punto(s) de Selva extra al identificar correctamente el residuo.";
        }
    }
    
    [RelayCommand]
    private async Task Terminate()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}");
    }
    
    [RelayCommand]
    private async Task Continue()
    {
        await NavigationService.NavigateAsync(nameof(LensesPageViewModel));
    }
}