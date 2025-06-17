using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLiteNetExtensions.Attributes;

namespace BabyNanny.Models
{
    using SQLite;

    [Table("BabyAction")]
    public class BabyAction
    {
        public enum FeedingTypes
        {
            Bottle,
            Meal,
            LeftBreast,
            RightBreast
        }

        [PrimaryKey, AutoIncrement, Column("Id")]
        public int Id { get; set; }
        public int Type { get; set; }
        public DateTime? Started { get; set; }
        public DateTime? Stopped { get; set; }

        public FeedingTypes? FeedingType { get; set; }
        public int? AmountML { get; set; }
        public string? BottleType { get; set; }
        public string? MealDescription { get; set; }

        [ForeignKey(typeof(Child))]
        public int ChildId { get; set; }

    }
}
