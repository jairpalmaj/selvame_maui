using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Alerts;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;

namespace ReciclaMe.Features.Classification;

public sealed partial class ClassificationCategoryPageViewModel : BaseViewModel
{
    private ModelClassification _modelClassification;
    private readonly IProfileRepository _profileRepository;
    private readonly ICategoryClassRepository _categoryClassRepository;
    
    [ObservableProperty]
    private string _imageSource;
    
    [ObservableProperty]
    private string _characterImageSource;
    
    public ClassificationCategoryPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository,
        ICategoryClassRepository categoryClassRepository) : base(navigationService, alertService)
    {
        _categoryClassRepository = categoryClassRepository;
        _profileRepository = profileRepository;
    }

    public override async Task OnAppearing()
    {
        Profile = await _profileRepository.GetAsync();
        var character = await _profileRepository.GetCharacterAsync();
        CharacterImageSource = character.ImageSource;
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ModelClassification", out var modelClassification))
        {
            _modelClassification = (ModelClassification) modelClassification;
            ImageSource =  _modelClassification.ImagePath;
        }
    }
    
    [RelayCommand]
    private async Task Validate(string categoryId)
    {
        var categoryInt = int.Parse(categoryId);
        var selectedCategory = await _categoryClassRepository.GetCategoryByClassId(categoryInt);
        var parameters = new Dictionary<string, object>
        {
            { "ModelClassification", _modelClassification },
            { "SelectedCategory", selectedCategory }
        };
        
        if (_modelClassification.CategoryClass.Category == selectedCategory.Category)
        {
            await _profileRepository.SaveProgressAsync(GamePoints.ExtraPoint);
            await NavigationService.NavigateAsync($"{nameof(ExtraPointsPageViewModel)}", parameters);
        }
        else
        {
            await NavigationService.NavigateAsync($"{nameof(FailedExtraPointsPageViewModel)}", parameters);
        }
    }

    protected override async Task Back()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(LensesPageViewModel)}");
    }
}