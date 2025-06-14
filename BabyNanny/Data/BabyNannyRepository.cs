using BabyNanny.Models;
using SQLite;
using SQLiteNetExtensions.Extensions;

namespace BabyNanny.Data
{
    public class BabyNannyRepository(string dbPath)
    {
        private SQLiteConnection? _connection;

        public void Init()
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.CreateTable<Child>();
            _connection.CreateTable<BabyAction>();
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
            return _connection?.Table<BabyAction>().OrderByDescending(x=>x.Started).ToList();
        }

        public void AddAction(BabyAction? action)
        {
            _connection = new SQLiteConnection(dbPath);
            _connection.Insert(action);
      
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