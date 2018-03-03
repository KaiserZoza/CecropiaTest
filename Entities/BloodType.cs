using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class BloodType
    {
        [Key]
        public Guid ID { get; set; }
        [StringLength(1)]
        public string RH { get; set; }
        [StringLength(1)]
        public string Type { get; set; }
    }
}
