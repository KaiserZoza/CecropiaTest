using Entities;
using System;
using System.Collections.Generic;

namespace CecropiaWebApplication.Models
{
    public class PatientDetailsViewModel
    {
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SelectedNationality { get; set; }
        public string Diseases { get; set; }
        public string PhoneNumber { get; set; }
        public string SelectedBloodType { get; set; }
    }
}