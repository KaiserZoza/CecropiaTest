using Entities;
using System;
using System.Collections.Generic;

namespace Logic
{
    public interface IPatientService
    {
        void CreatePatient(Patient patient);
        Patient GetPatient(Guid ID);
        IEnumerable<Patient> GetPatients();
        void UpdatePatient(Patient patient);
        void DeletePatient(Patient patient);
        IEnumerable<Country> GetCountries();
        Country GetCountry(string code);
        IEnumerable<Object> GetBloodTypes();
        BloodType GetBloodType(Guid ID);
    }
}
