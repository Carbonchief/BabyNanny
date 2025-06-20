using Microsoft.Maui.Storage;
using System;

namespace BabyNanny.Data
{
    public class ChildStateService
    {
        private const string PreferenceKey = "SelectedChildId";

        public event Action? ChildChanged;

        public int SelectedChildId { get; private set; }

        public ChildStateService()
        {
            SelectedChildId = Preferences.Default.Get(PreferenceKey, 0);
        }

        public void SetChild(int id)
        {
            SelectedChildId = id;
            Preferences.Default.Set(PreferenceKey, id);
            ChildChanged?.Invoke();
        }
    }
}
