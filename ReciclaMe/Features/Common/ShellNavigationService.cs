namespace ReciclaMe.Features.Common;

public class ShellNavigationService : INavigationService
{
    public virtual async Task GoBackAsync()
    {
        await Shell.Current.GoToAsync("..");
    }

    public virtual async Task GoBackAsync(IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync("..", parameters);
    }

    public virtual async Task NavigateAsync(string path)
    {
        await Shell.Current.GoToAsync(path);
    }

    public virtual async Task NavigateAsync(string path, IDictionary<string, object> parameters)
    {
        await Shell.Current.GoToAsync(path, parameters);
    }
}
