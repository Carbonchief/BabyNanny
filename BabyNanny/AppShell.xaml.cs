using BabyNanny.Data;

namespace BabyNanny;

public partial class AppShell : Shell
{
    public AppShell(ChildState childState)
    {
        InitializeComponent();
        BindingContext = childState;
    }

    private async void OnSettingsClicked(object? sender, EventArgs e)
    {
        await GoToAsync("settings");
    }
}
