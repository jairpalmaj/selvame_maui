using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Alerts;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Learn;
using ReciclaMe.Features.Menu;
using ReciclaMe.Infrastructure;

namespace ReciclaMe.Features.ExplorerLenses;

public sealed partial class LearningLensesPageViewModel : BaseViewModel
{
    private readonly ICategoryClassRepository _categoryClassRepository;
    private readonly IClassificationModelService _classificationModelService;
    private readonly ICameraProvider _cameraProvider;
    private readonly IImageService _imageService;

    [ObservableProperty] 
    private bool _isEnabled;
    
    [ObservableProperty] 
    private bool _isRunning;
    
    [ObservableProperty] 
    public partial CameraInfo? SelectedCamera { get; set; }

    public LearningLensesPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        ICategoryClassRepository categoryClassRepository,
        IClassificationModelService classificationModelService,
        ICameraProvider cameraProvider,
        IImageService imageService) : base(navigationService, alertService)
    {
        _cameraProvider = cameraProvider;
        _categoryClassRepository = categoryClassRepository;
        _classificationModelService =  classificationModelService;
        _imageService = imageService;
        IsEnabled = true;
    }
    
    [RelayCommand]
    private async Task CapturePicture(Stream pictureStream)
    {
        IsEnabled = false;
        IsRunning = true;
        var filePath = _imageService.SavePicture(pictureStream);
        var modelClassification = await _classificationModelService.GetClassificationAsync();
        modelClassification.ImagePath = filePath;
        
        var parameters = new Dictionary<string, object>
        {
            { "ModelClassification", modelClassification }
        };
        await NavigationService.NavigateAsync(nameof(LearnPageViewModel), parameters);
        IsEnabled = true;
        IsRunning = false;
    }

    [RelayCommand]
    private async Task Validate()
    {
        var classes = await _categoryClassRepository.GetCategoriesAsync();
        var classLabels = classes.Select(c => c.Category.ToString()).ToArray();
        
        const string cancel = "Cancelar";
        const string mystery = "Mystery Object";
        var classification = await AlertService.DisplayActionSheetAsync("Elige una clase para probar:", mystery, cancel, classLabels);
        
        if (cancel == classification)
        {
            return;
        }
        
        //check for mystery for 
        //simulate accuracy
        if (mystery == classification)
        {
            await NavigationService.NavigateAsync(nameof(MysteryObjectPageViewModel));
            return;
        }
        
        Category.TryParse(classification, out Category category);
        var modelClassification = await _classificationModelService.GetClassificationAsync(category);

        //validate accuracy
        if (modelClassification.Accuracy > 0.61)
        {
            var parameters = new Dictionary<string, object>
            {
                { "ModelClassification", modelClassification }
            };
            await NavigationService.NavigateAsync(nameof(LearnPageViewModel), parameters);
        }
        else
        {
            await NavigationService.NavigateAsync(nameof(MysteryObjectPageViewModel));
        }
    }
    
    [RelayCommand]
    private async Task Fail()
    {
        await NavigationService.NavigateAsync($"{nameof(MysteryObjectPageViewModel)}");
    }

    protected override async Task Back()
    {
        await NavigationService.NavigateAsync($"///{MainMenuRootPath}");
    }
}