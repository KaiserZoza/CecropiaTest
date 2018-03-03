using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Country
    {
        [Key]
        [StringLength(3)]
        public string Code { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
