using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Features.Menu;

namespace ReciclaMe.Features.Alerts;

public partial class SuccessPageViewModel : BaseViewModel
{
    private ModelClassification _modelClassification;

    [ObservableProperty]
    private string _imageSource;
    
    [ObservableProperty]
    private string _categoryName;
    
    [ObservableProperty]
    private string _pointsLabel;
    
    public SuccessPageViewModel(INavigationService navigationService,
        IAlertService alertService) : base(navigationService, alertService)
    {
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
    
    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ModelClassification", out var modelClassification))
        {
            _modelClassification = (ModelClassification) modelClassification;
            ImageSource =  _modelClassification.ImagePath;
            CategoryName = $"¡Es {_modelClassification.CategoryClass.Title}!";
            PointsLabel = $"{GamePoints.Success}+ Puntos de Selva";
        }
    }
}