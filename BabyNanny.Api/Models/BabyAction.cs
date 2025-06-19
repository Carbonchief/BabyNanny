using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyNanny.Api.Models
{
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

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Type { get; set; }

        public DateTime? Started { get; set; }

        public DateTime? Stopped { get; set; }

        public FeedingTypes? FeedingType { get; set; }

        public int? AmountML { get; set; }

        public string? BottleType { get; set; }

        public string? MealDescription { get; set; }

        public string? DiaperType { get; set; }

        [ForeignKey(nameof(Child))]
        public int ChildId { get; set; }

        public Child? Child { get; set; }
    }
}
