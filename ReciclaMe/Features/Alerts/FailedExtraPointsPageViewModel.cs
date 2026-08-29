using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Features.Menu;

namespace ReciclaMe.Features.Alerts;

public sealed partial class FailedExtraPointsPageViewModel : BaseViewModel
{
    private ModelClassification _modelClassification;
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private string _characterName;
    
    [ObservableProperty]
    private string _characterImageSource;
    
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
    
    public FailedExtraPointsPageViewModel(INavigationService navigationService, 
        IAlertService alertService,
        IProfileRepository profileRepository) : base(navigationService, alertService)
    {
        _profileRepository = profileRepository;
    }

    public override async Task OnAppearing()
    {
        var character = await _profileRepository.GetCharacterAsync();
        CharacterName = character.Name;
        CharacterImageSource = character.ImageSource;
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("SelectedCategory", out var selectedCategory))
        {
            var category = (CategoryClass) selectedCategory;
            NotCategoryName = $"No {category.Title}";
        }
        
        if (query.TryGetValue("ModelClassification", out var modelClassification))
        {
            _modelClassification = (ModelClassification) modelClassification;
            ImageSource =  _modelClassification.ImagePath;
            CategoryName = $"¡Es {_modelClassification.CategoryClass.Title}!";
            PointsLabel = $"{GamePoints.Success}+ Puntos de Selva";
            PointsInformation = $"No ganaste los puntos extra esta vez. \n ¡Continúa explorando!";
        }
    }
    
    [RelayCommand]
    private async Task Continue()
    {
        await NavigationService.NavigateAsync($"{nameof(LensesPageViewModel)}");
    }
    
    [RelayCommand]
    private async Task GoToBase()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}");
    }
}