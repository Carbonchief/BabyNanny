using BabyNanny.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;
using System.Linq;

namespace BabyNanny.Data
{
    public class BabyNannyRepository(string dbPath)
    {
        private readonly string _dbPath = dbPath;

        public void Init()
        {
            using var _ = GetConnection();
        }

        private SQLiteConnection GetConnection()
        {
            var connection = new SQLiteConnection(_dbPath);
            connection.CreateTable<Child>();
            connection.CreateTable<BabyAction>();

            var tableInfo = connection.GetTableInfo(nameof(BabyAction));
            var columns = tableInfo.Select(c => c.Name).ToList();
            if (!columns.Contains(nameof(BabyAction.FeedingType)))
                connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.FeedingType)} INTEGER");
            if (!columns.Contains(nameof(BabyAction.AmountML)))
                connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.AmountML)} INTEGER");
            if (!columns.Contains(nameof(BabyAction.BottleType)))
                connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.BottleType)} TEXT");
            if (!columns.Contains(nameof(BabyAction.MealDescription)))
                connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.MealDescription)} TEXT");
            if (!columns.Contains(nameof(BabyAction.DiaperType)))
                connection.Execute($"ALTER TABLE {nameof(BabyAction)} ADD COLUMN {nameof(BabyAction.DiaperType)} TEXT");
            return connection;
        }

        #region Child

        public List<Child>? GetChildren()
        {
            using var db = GetConnection();
            return db.GetAllWithChildren<Child>();
        }

        public Child AddChild(Child child)
        {
            using var db = GetConnection();
            db.Insert(child);
            return child;
        }

        public void UpdateChild(Child child)
        {
            using var db = GetConnection();
            db.Update(child);
        }

        public void DeleteChild(int id)
        {
            using var db = GetConnection();
            db.Delete<Child>(id);
        }

        #endregion

        #region Action

        public List<BabyAction>? GetActions()
        {
            using var db = GetConnection();
            return db.Table<BabyAction>().OrderByDescending(x => x.Started).ToList();
        }

        public bool AddAction(BabyAction? action)
        {
            if (action == null)
                return false;

            using var db = GetConnection();

            var existing = db.Table<BabyAction>()
                .FirstOrDefault(a => a.ChildId == action.ChildId && a.Started != null && a.Stopped == null);

            if (existing != null)
                return false;

            db.Insert(action);
            return true;

        }

        public void EditAction(BabyAction action)
        {
            using var db = GetConnection();
            db.Update(action);

        }

        public void DeleteAction(int id)
        {
            using var db = GetConnection();
            db.Delete<BabyAction>(id);
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