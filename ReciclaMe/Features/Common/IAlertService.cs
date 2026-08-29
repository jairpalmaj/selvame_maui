namespace ReciclaMe.Features.Common;

public interface IAlertService
{
    
    Task<string> DisplayPromptAsync(string title, string message, string ok = "OK", string cancel = "Cancel", string placeholder = "", int maxLength = -1, Keyboard? keyboard = null, string initialValue = "");
    Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);
    Task DisplayAlertAsync(string title, string message, string confirm = "OK");
    Task<bool> DisplayAlertAsync(string title, string message, string confirm, string cancel);
}