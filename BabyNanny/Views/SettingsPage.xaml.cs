using BabyNanny.Data;
using BabyNanny.Models;
using Microsoft.Maui.Controls;
using System.Linq;

namespace BabyNanny.Views;

public partial class SettingsPage : ContentPage
{
    private readonly BabyNannyRepository _repository;
    private readonly ChildState _childState;

    public SettingsPage()
    {
        InitializeComponent();
        _repository = App.BabyNannyRepository!;
        _childState = App.ChildState!;
        BindingContext = _childState;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_childState.Children.Count == 0)
        {
            var list = _repository.GetChildren();
            if (list != null)
                _childState.Children = list;
        }
    }

    private async void OnAddChild(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("addchild");
    }

    private async void OnEditChild(object sender, EventArgs e)
    {
        if ((sender as BindableObject)?.BindingContext is Child child)
        {
            await Shell.Current.GoToAsync($"addchild?id={child.Id}");
        }
    }

    private async void OnDeleteChild(object sender, EventArgs e)
    {
        if ((sender as BindableObject)?.BindingContext is Child child)
        {
            bool confirm = await DisplayAlert("Delete Child", $"Delete {child.Name}?", "Delete", "Cancel");
            if (confirm)
            {
                _repository.DeleteChild(child.Id);
                _childState.RemoveChild(child.Id);
            }
        }
    }
}
