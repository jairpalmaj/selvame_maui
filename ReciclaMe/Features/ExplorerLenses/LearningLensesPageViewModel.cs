using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Features.Alerts;
using ReciclaMe.Features.Common;
using ReciclaMe.Features.Learn;

namespace ReciclaMe.Features.ExplorerLenses;

public sealed partial class LearningLensesPageViewModel : BaseViewModel
{
    private readonly ICategoryClassRepository _categoryClassRepository;
    private readonly IClassificationModelService _classificationModelService;
    
    public LearningLensesPageViewModel(INavigationService navigationService,
        IAlertService alertService,
        ICategoryClassRepository categoryClassRepository,
        IClassificationModelService classificationModelService) : base(navigationService, alertService)
    {
        _categoryClassRepository = categoryClassRepository;
        _classificationModelService =  classificationModelService;
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
}