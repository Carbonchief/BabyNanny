using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BabyNanny.Api.Models
{
    [Table("Child")]
    public class Child
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public DateTime? Birthday { get; set; }

        public ICollection<BabyAction>? Actions { get; set; }
    }
}
