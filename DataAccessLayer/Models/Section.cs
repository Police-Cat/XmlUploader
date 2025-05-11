using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class Section
    {
        [Key]
        public Guid Id { get; set; }

        public ICollection<Param> Params { get; set; } = new List<Param>();
    }
}
