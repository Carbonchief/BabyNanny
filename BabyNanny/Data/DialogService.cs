namespace BabyNanny.Data
{
    internal class DialogService : IDialogService
    {
        public async Task<string> DisplayPrompt(string title, string message)
        {
            return await Application.Current!.Windows[0].Page!.DisplayPromptAsync(title, message);
        }

        public async Task DisplayAlert(string title, string message, string cancel)
        {
            await Application.Current!.Windows[0].Page!.DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await Application.Current!.Windows[0].Page!.DisplayAlert(title, message, accept, cancel);
        }

        public async Task<string?> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await Application.Current!.Windows[0].Page!.DisplayActionSheet(title, cancel, destruction, buttons);
        }
    }
}
