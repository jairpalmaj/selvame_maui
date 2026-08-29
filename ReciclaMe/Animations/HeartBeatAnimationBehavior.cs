namespace ReciclaMe.Animations;

public sealed class HeartBeatAnimationBehavior : Behavior<VisualElement>
{
    private bool _isAnimating;
    protected override async void OnAttachedTo(VisualElement bindable)
    {
        base.OnAttachedTo(bindable);
        _isAnimating = true;
        await StartPulsingAnimation(bindable);
    }
    
    private async Task StartPulsingAnimation(VisualElement view)
    {
        while (_isAnimating)
        {
            // Zoom In
            await view.ScaleToAsync(1.1, 1000, Easing.SinInOut);
            
            // Zoom Out
            await view.ScaleToAsync(1.0, 1000, Easing.SinInOut);
        }
    }

    protected override void OnDetachingFrom(VisualElement bindable)
    {
        _isAnimating = false;
        bindable.CancelAnimations();
        base.OnDetachingFrom(bindable);
    }
}