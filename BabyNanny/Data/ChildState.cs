using BabyNanny.Models;
using System.ComponentModel;

namespace BabyNanny.Data;

public class ChildState : INotifyPropertyChanged
{
    private Child? _selectedChild;
    public event PropertyChangedEventHandler? PropertyChanged;

    public Child? SelectedChild
    {
        get => _selectedChild;
        set
        {
            if (_selectedChild != value)
            {
                _selectedChild = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedChild)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedChildName)));
            }
        }
    }

    public string SelectedChildName => _selectedChild?.Name ?? string.Empty;
}
