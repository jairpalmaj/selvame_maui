namespace ReciclaMe.Features.Common;

public partial class BasePage : ContentPage
{
    public BasePage()
    {
        InitializeComponent();
    }

    protected override bool OnBackButtonPressed()
    {
        return true;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        var vm = (IViewModelLifeCycle) BindingContext;
        await vm.OnAppearing();
    }

    protected override async void OnDisappearing()
    {
        base.OnDisappearing();
        var vm = (IViewModelLifeCycle) BindingContext;
        await vm.OnDisappearing();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        var vm = (IViewModelLifeCycle) BindingContext;
        await vm.OnNavigatedTo();
    }

    protected override async void OnNavigatedFrom(NavigatedFromEventArgs args)
    {
        var vm = (IViewModelLifeCycle) BindingContext;
        await vm.OnNavigatedFrom();
    }
}