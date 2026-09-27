using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Services;

namespace ReciclaMe.Features.StartAdventure;

public sealed partial class ChooseCharacterPageViewModel : BaseViewModel
{
    private readonly IProfileRepository _profileRepository;
    
    [ObservableProperty]
    private bool _isEnable;
    
    [ObservableProperty]
    private bool _isCharacter1Selected;
    
    [ObservableProperty]
    private bool _isCharacter2Selected;
    
    [ObservableProperty]
    private bool _isCharacter3Selected;
    
    [ObservableProperty]
    private bool _isCharacter4Selected;
    
    private int _characterId;
    
    public ChooseCharacterPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IProfileRepository profileRepository, 
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        _profileRepository = profileRepository;
    }
    
    [RelayCommand]
    private void SelectCharacter(string characterId)
    {
        IsEnable = true;
        _characterId = int.Parse(characterId);
        ChangeStatus(_characterId);
    }

    private void ChangeStatus(int characterId)
    {
        switch (characterId)
        {
            case 1:
                IsCharacter1Selected = true;
                IsCharacter2Selected = false;
                IsCharacter3Selected = false;
                IsCharacter4Selected = false;
                break;
            case 2:
                IsCharacter1Selected = false;
                IsCharacter2Selected = true;
                IsCharacter3Selected = false;
                IsCharacter4Selected = false;
                break;
            case 3:
                IsCharacter1Selected = false;
                IsCharacter2Selected = false;
                IsCharacter3Selected = true;
                IsCharacter4Selected = false;
                break;
            case 4:
                IsCharacter1Selected = false;
                IsCharacter2Selected = false;
                IsCharacter3Selected = false;
                IsCharacter4Selected = true;
                break;
        }
    }

    [RelayCommand]
    private async Task Start()
    {
        var app = App.Current as App;
        var profile = new UserProfile
        {
            CharacterId = _characterId,
        };
        //we track the character selected
        AnalyticsService.Count(AnalyticsKeys.CharacterSelection, 1, [
            new KeyValuePair<string, object>("CharacterId", _characterId)
        ]);
        await _profileRepository.SaveAsync(profile);
        await _profileRepository.SaveCharacterAsync(_characterId);
        app?.NavigateToMenu();
    }
}