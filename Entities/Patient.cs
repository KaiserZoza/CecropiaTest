using System;
using System.ComponentModel.DataAnnotations;

namespace Entities
{
    public class Patient
    {
        [Key]
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Country Nationality { get; set; }
        public string Diseases {get;set;}
        public string PhoneNumber { get; set; }
        public BloodType BloodType { get; set; }
    }
}
