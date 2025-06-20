using BabyNanny.Data;
using BabyNanny.Models;
using Microsoft.Maui.Controls;
using System.Linq;

namespace BabyNanny.Views;

[QueryProperty(nameof(ChildId), "id")]
public partial class AddChildPage : ContentPage
{
    private readonly BabyNannyRepository _repository;
    private readonly ChildState _childState;
    private Child _child = new();

    public int ChildId { get; set; }

    public AddChildPage()
    {
        InitializeComponent();
        _repository = App.BabyNannyRepository!;
        _childState = App.ChildState!;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (ChildId != 0)
        {
            var child = _childState.Children.FirstOrDefault(c => c.Id == ChildId) ??
                         _repository.GetChildren()?.FirstOrDefault(c => c.Id == ChildId);
            if (child != null)
            {
                _child = new Child { Id = child.Id, Name = child.Name, Birthday = child.Birthday };
                nameEntry.Text = _child.Name;
                if (child.Birthday.HasValue)
                    birthdayPicker.Date = child.Birthday.Value;
            }
        }
        else
        {
            _child = new Child();
            nameEntry.Text = string.Empty;
            birthdayPicker.Date = DateTime.Today;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        _child.Name = nameEntry.Text;
        _child.Birthday = birthdayPicker.Date;

        var isNew = _child.Id == 0;
        if (isNew)
        {
            _repository.AddChild(_child);
        }
        else
        {
            _repository.UpdateChild(_child);
        }

        _childState.AddOrUpdateChild(_child, select: isNew);
        await Shell.Current.GoToAsync("..");
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
