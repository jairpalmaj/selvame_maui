using System;
using System.Collections.Generic;
using System.Diagnostics;
using ReciclaMe.Features.Common;

namespace ReciclaMe.Features.ExplorerLenses;

public partial class LearningLensesPage : BasePage
{
    private CancellationTokenSource _cancellationTokenSource;
    private LearningLensesPageViewModel _vm;
    public LearningLensesPage(LearningLensesPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = _vm = vm;
        _cancellationTokenSource = new CancellationTokenSource();
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
                var captureImageCTS = new CancellationTokenSource(TimeSpan.FromSeconds(3));
                await using Stream stream = await Camera.CaptureImage(captureImageCTS.Token);
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