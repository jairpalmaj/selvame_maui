using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Alerts;

namespace ReciclaMe.Features.Classification;

public partial class ClassificationPageViewModel : BaseViewModel
{
    private ModelClassification _modelClassification;
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private int _points;
    
    [ObservableProperty]
    private string _imageSource;
    
    [ObservableProperty]
    private string _characterImageSource;
    
    public ClassificationPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository) : base(navigationService, alertService)
    {
        _profileRepository = profileRepository;
    }
    
    public override async Task OnAppearing()
    {
        Profile = await _profileRepository.GetAsync();
        Points = Profile.Points;
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
    private async Task ValidateRecyclable()
    {
        var parameters = new Dictionary<string, object>
        {
            { "ModelClassification", _modelClassification }
        };
        
        if (_modelClassification.CategoryClass.IsRecyclable)
        {

            //Correct
            await _profileRepository.SaveProgressAsync(GamePoints.Success);
            await NavigationService.NavigateAsync($"{nameof(ClassificationCategoryPageViewModel)}", parameters);
        }
        else
        {
            //incorrect
            //Save points
            await _profileRepository.SaveProgressAsync(GamePoints.Fail);
            await NavigationService.NavigateAsync($"{nameof(FailedPageViewModel)}", parameters);
        }
    }

    [RelayCommand]
    private async Task ValidateNoRecyclable()
    {
        var parameters = new Dictionary<string, object>
        {
            { "ModelClassification", _modelClassification }
        };
        
        if (_modelClassification.CategoryClass.IsRecyclable)
        {
            //Incorrect
            //Save points
            await _profileRepository.SaveProgressAsync(GamePoints.Fail);
            await NavigationService.NavigateAsync($"{nameof(FailedPageViewModel)}", parameters);
        }
        else
        {
            //Correct
            //Save points
            await _profileRepository.SaveProgressAsync(GamePoints.Success);
            await NavigationService.NavigateAsync($"{nameof(SuccessPageViewModel)}", parameters);
        }
    }
}