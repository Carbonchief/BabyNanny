namespace BabyNanny.Data;

internal interface IDialogService
{
    Task<string> DisplayPrompt(string title, string message);
    Task DisplayAlert(string title, string message, string cancel);
    Task<bool> DisplayAlert(string title, string message, string accept, string cancel);
    Task<string?> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons);
}
