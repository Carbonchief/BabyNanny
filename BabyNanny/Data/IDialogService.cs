namespace BabyNanny.Data;

internal interface IDialogService
{
    Task<string> DisplayPrompt(string title,string message);
    Task<bool> DisplayAlert(string title,string message,string accept ,string cancel);
}