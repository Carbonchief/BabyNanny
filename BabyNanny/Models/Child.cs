using SQLite;
using SQLiteNetExtensions.Attributes;

namespace BabyNanny.Models
{
    [Table("Child")]
    public class Child
    {
        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? Birthday { get; set; }

        [OneToMany]
        public List<BabyAction>? Actions { get; set; }
    }
}
