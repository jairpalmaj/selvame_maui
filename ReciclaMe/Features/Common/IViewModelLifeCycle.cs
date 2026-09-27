namespace ReciclaMe.Features.Common;

public interface IViewModelLifeCycle
{
    Task OnAppearing();
    Task OnDisappearing();
    Task OnNavigatedTo();
    Task OnNavigatedFrom();
}