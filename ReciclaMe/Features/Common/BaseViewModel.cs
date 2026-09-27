using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReciclaMe.Domain;
using ReciclaMe.Services;

namespace ReciclaMe.Features.Common;

public partial class BaseViewModel : ObservableObject, IQueryAttributable, IViewModelLifeCycle
{
    protected readonly INavigationService NavigationService;
    protected readonly IAlertService AlertService;
    protected static UserProfile? Profile;
    protected readonly IAnalyticsService AnalyticsService;
    protected readonly ICrashReportService CrashReportService;
    
    /// <summary>
    /// This is the main root for menu page
    /// shell
    /// </summary>
    public const string MainMenuRootPath = "MenuPage";
    

    public BaseViewModel(INavigationService navigationService,
        IAlertService alertService,
        IAnalyticsService analyticsService,
        ICrashReportService crashReportService)
    {
        NavigationService = navigationService;
        AlertService = alertService;
        AnalyticsService = analyticsService;
        CrashReportService = crashReportService;
    }

    [RelayCommand]
    protected virtual async Task Back()
    {
        await NavigationService.GoBackAsync();
    }

    public virtual void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        
    }

    public virtual Task OnAppearing()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnDisappearing()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatedTo()
    {
        return Task.CompletedTask;
    }

    public virtual Task OnNavigatedFrom()
    {
        return Task.CompletedTask;
    }
}