using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Classification;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Learn;
using ReciclaMe.Features.Menu;

namespace ReciclaMe.Features.Alerts;

public sealed partial class FailedPageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private int _points;
    
    [ObservableProperty]
    private string _pointsLabel;
    
    [ObservableProperty]
    private string _characterImageSource;
    
    [ObservableProperty]
    private string _itemInformation;

    private ModelClassification _modelClassification;
    
    public FailedPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository) : base(navigationService, alertService)
    {
        _profileRepository = profileRepository;
    }

    public override async Task OnAppearing()
    {
        Profile = await _profileRepository.GetAsync();
        var character = await _profileRepository.GetCharacterAsync();
        CharacterImageSource = character.ImageSource;
        Points = Profile.Points;
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        PointsLabel = $"{GamePoints.Fail} Puntos de Selva";
        
        if (query.TryGetValue("ModelClassification", out var modelClassification))
        {
            _modelClassification = (ModelClassification) modelClassification;
            ItemInformation =  _modelClassification?.CategoryClass?.CategoryInformation;
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
        var parameters = new Dictionary<string, object>
        {
            { "ModelClassification", _modelClassification }
        };
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(ClassificationPageViewModel)}", parameters);
    }
}