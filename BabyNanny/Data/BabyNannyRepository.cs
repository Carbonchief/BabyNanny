using BabyNanny.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;

namespace BabyNanny.Data
{
    public class BabyNannyRepository(string dbPath, HttpClient httpClient)
    {
        private readonly HttpClient _httpClient = httpClient;
        private SQLiteConnection? _connection;

        public void Init()
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Child>();
            _connection.CreateTable<BabyAction>();

            var tableInfo = _connection.GetTableInfo(nameof(BabyAction));
            var columns = tableInfo.Select(c => c.Name).ToList();
            if (!columns.Contains(nameof(BabyAction.FeedingType)))
                _connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.FeedingType)} INTEGER");
            if (!columns.Contains(nameof(BabyAction.AmountML)))
                _connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.AmountML)} INTEGER");
            if (!columns.Contains(nameof(BabyAction.BottleType)))
                _connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.BottleType)} TEXT");
            if (!columns.Contains(nameof(BabyAction.MealDescription)))
                _connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.MealDescription)} TEXT");
            if (!columns.Contains(nameof(BabyAction.DiaperType)))
                _connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.DiaperType)} TEXT");
        }

        #region Child

        public List<Child>? GetChildren()
        {
            Init();
            return _connection.GetAllWithChildren<Child>();
            //return _connection?.Table<Child>().ToList();

        }

        public Child AddChild(Child child)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.Insert(child);
            return child;
        }

        public async Task<Child?> CreateChildAsync(Child child)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/children", child);
            if (!response.IsSuccessStatusCode) return null;
            var created = await response.Content.ReadFromJsonAsync<Child>();
            if (created != null)
            {
                _connection = new SQLiteConnection(dbPath);
                _connection.Insert(created);
            }
            return created;
        }

        public async Task<bool> UpdateChildAsync(Child child)
        {
            var response = await _httpClient.PutAsJsonAsync($"/api/children/{child.Id}", child);
            if (!response.IsSuccessStatusCode) return false;
            _connection = new SQLiteConnection(dbPath);
            _connection.Update(child);
            return true;
        }

        public void DeleteChild(int id)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.Delete<Child>(id);
        }

        #endregion

        #region Action

        public List<BabyAction>? GetActions()
        {
            Init();
            return _connection?.Table<BabyAction>().OrderByDescending(x => x.Started).ToList();
        }

        public bool AddAction(BabyAction? action)
        {
            if (action == null)
                return false;

            _connection = new SQLiteConnection(dbPath);

            var existing = _connection.Table<BabyAction>()
                .FirstOrDefault(a => a.ChildId == action.ChildId && a.Started != null && a.Stopped == null);

            if (existing != null)
                return false;

            _connection.Insert(action);
            return true;

        }

        public void EditAction(BabyAction action)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.Update(action);

        }

        public void DeleteAction(int id)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.Delete<BabyAction>(id);
        }

        #endregion

        public enum ActionTypes
        {
            Feeding,
            Sleeping,
            Diaper
        }
    }
}