using CommunityToolkit.Mvvm.ComponentModel;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Services;

namespace ReciclaMe.Features.ExplorerLenses;

public sealed partial class ExplanationLensesPageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private string _characterName;
    
    [ObservableProperty]
    private string _characterImageSource;
    
    [ObservableProperty]
    private string _advice;
    
    public ExplanationLensesPageViewModel(INavigationService navigationService, 
        IProfileRepository profileRepository,       
        IAlertService alertService, 
        IAnalyticsService analyticsService, 
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        _profileRepository = profileRepository;
    }
    
    public override async Task OnAppearing()
    {
        var character = await _profileRepository.GetCharacterAsync();
        CharacterName = character.Name;
        CharacterImageSource = character.ImageSource;
        Advice = $"Consejo de {CharacterName}: ";
    }
}