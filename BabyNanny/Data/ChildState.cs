using BabyNanny.Models;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;

namespace BabyNanny.Data;

public class ChildState : INotifyPropertyChanged
{
    private Child? _selectedChild;
    private List<Child> _children = new();

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


    public List<Child> Children
    {
        get => _children;
        set
        {
            if (_children != value)
            {
                _children = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Children)));
            }
        }
    }

    public string SelectedChildName => _selectedChild?.Name ?? string.Empty;

    public void AddOrUpdateChild(Child child, bool select = false)
    {
        var existing = _children.FirstOrDefault(c => c.Id == child.Id);
        if (existing == null)
        {
            _children.Add(child);
        }
        else
        {
            existing.Name = child.Name;
            existing.Birthday = child.Birthday;
        }

        if (select)
            SelectedChild = child;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Children)));
    }

    public void RemoveChild(int id)
    {
        var child = _children.FirstOrDefault(c => c.Id == id);
        if (child == null)
            return;

        _children.Remove(child);
        if (_selectedChild?.Id == id)
            SelectedChild = null;

        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Children)));
    }
}
