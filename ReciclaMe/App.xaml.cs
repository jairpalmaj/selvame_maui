using Microsoft.Extensions.DependencyInjection;

namespace ReciclaMe;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new StartAppShell());
    }

    public void NavigateToMenu()
    {
        Current!.Windows[0].Page = new MenuAppShell();
    }
    
    public void NavigateToChoosePage()
    {
        Current!.Windows[0].Page = new ChooseAppShell();
    }
}