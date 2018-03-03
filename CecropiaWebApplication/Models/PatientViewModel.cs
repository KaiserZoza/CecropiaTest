using Entities;
using System;
using System.Collections.Generic;

namespace CecropiaWebApplication.Models
{
    public class PatientViewModel
    {
        public PatientViewModel() { }
        public PatientViewModel(IEnumerable<Country> countries, IEnumerable<Object> bloodTypes)
        {
            Countries = countries;
            BloodTypes = bloodTypes;
        }
        public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string SelectedNationality { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public string Diseases { get; set; }
        public string PhoneNumber { get; set; }
        public Guid SelectedBloodType { get; set; }
        public IEnumerable<Object> BloodTypes { get; set; }
    }
}