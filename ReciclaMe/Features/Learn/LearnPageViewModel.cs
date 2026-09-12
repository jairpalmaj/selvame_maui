using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.ExplorerLenses;
using ReciclaMe.Features.Menu;
using ReciclaMe.Infrastructure;
using ReciclaMe.Services;

namespace ReciclaMe.Features.Learn;

public sealed partial class LearnPageViewModel : BaseViewModel
{
    private readonly IImageService _imageService;

    private ModelClassification _categoryClass;
    
    [ObservableProperty]
    private string? _title;
    
    [ObservableProperty]
    private string? _imageSource;
    
    [ObservableProperty]
    private string? _photoSource;
    
    [ObservableProperty]
    private string? _recyclableLabel;
    
    [ObservableProperty]
    private string? _recyclableImage;
    
    [ObservableProperty]
    private string? _categoryInformation;
    
    [ObservableProperty]
    private string? _recyclableSummary;
    
    [ObservableProperty]
    private bool _isRecyclable;
    
    public LearnPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        IImageService imageService, 
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService) : base(navigationService, alertService, analyticsService, crashReportService)
    {
        _imageService =  imageService;
    }
    
    [RelayCommand]
    private async Task Understood()
    {
        //delete file
        _imageService.DeletePicture();
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}");
    }
    
    [RelayCommand]
    private async Task Continue()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}/{nameof(LearningLensesPageViewModel)}");
    }

    public override void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("ModelClassification", out var category))
        {
            _categoryClass = (ModelClassification) category;
            const string recyclabeSummary = "Este cofre guarda los artefactos que tienen la magia de transformarse. Si los depositamos aquí, nuestro equipo en la base los convertirá en cosas nuevas, ¡salvando miles de recursos naturales en nuestra selva!";
            const string noRecyclabeSummary = "¡Alerta, exploradores! En este cofre debemos encerrar los objetos que ya perdieron su magia y no pueden transformarse. Es vital aislarlos en esta zona de cuarentena para que no contaminen la tierra.";
            Title = $"¡Es {_categoryClass.CategoryClass.Title}!";
            IsRecyclable = _categoryClass.CategoryClass.IsRecyclable;
            RecyclableLabel = _categoryClass.CategoryClass.IsRecyclable ? "RECICLABLE" : "NO RECICLABLE";
            ImageSource = _categoryClass.ImagePath;
            CategoryInformation = _categoryClass.CategoryClass.CategoryInformation;
            RecyclableImage = _categoryClass.CategoryClass.IsRecyclable ? "trash_gray" : "trash_orange";
            RecyclableSummary = _categoryClass.CategoryClass.IsRecyclable ? recyclabeSummary : noRecyclabeSummary;
        }
    }
}