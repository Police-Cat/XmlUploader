using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Param
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        [Required]
        public string Value { get; set; } = null!;

        [ForeignKey("Section")]
        public Guid SectionId { get; set; }
        public Section Section { get; set; } = null!;
    }
}
