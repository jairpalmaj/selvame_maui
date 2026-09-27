using System;
using System.Diagnostics;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.ExplorerLenses;

public partial class LensesPage : BasePage
{
    private LensesPageViewModel _vm;
    private CancellationTokenSource _cancellationTokenSource;
    
    public LensesPage(LensesPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
        _cancellationTokenSource =  new CancellationTokenSource();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        try
        {
            Camera.StartCameraPreview(_cancellationTokenSource.Token);
        }
        catch(Exception ex)
        {
            Trace.WriteLine(ex);
        }
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        try
        {
            Camera.StopCameraPreview();
        }
        catch (Exception ex)
        {
            Trace.WriteLine(ex);
        }
    }

    private async void OnCapture(object? sender, TappedEventArgs e)
    {
        try
        {
            if (_vm.IsEnabled)
            {
                _vm.IsRunning = true;
                _vm.IsEnabled = false;
                var captureTokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(3));
                await using Stream stream = await Camera.CaptureImage(captureTokenSource.Token);
                _vm.CapturePictureCommand.Execute(stream);
            }
        }
        catch(Exception ex)
        {
            // Handle Exception
            Trace.WriteLine(ex);
        }
    }
}