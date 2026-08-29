namespace ReciclaMe.Features.Common;

public sealed class ShellAlertService : IAlertService
    {
        public async Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
        {
            return await Shell.Current.DisplayActionSheetAsync(title, cancel, destruction, buttons);
        }

        public async Task DisplayAlertAsync(string title, string message, string confirm = "Ok")
        {
            await Shell.Current.DisplayAlertAsync(title, message, confirm);
        }

        public async Task<bool> DisplayAlertAsync(string title, string message, string confirm, string cancel)
        {
            return await Shell.Current.DisplayAlertAsync(title, message, confirm, cancel);
        }

        public async Task<string> DisplayPromptAsync(string title, string message, string ok = "OK", string cancel = "Cancelar", string placeholder = "", int maxLength = -1, Keyboard? keyboard = null, string initialValue = "")
        {
            return await Shell.Current.DisplayPromptAsync(title, message, ok, cancel, placeholder, maxLength, keyboard, initialValue);
        }
    }