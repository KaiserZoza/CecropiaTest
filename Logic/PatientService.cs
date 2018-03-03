using AutoMapper;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class PatientService : IPatientService
    {
        IGenericRepository<Patient> _patientRepository;
        IGenericRepository<Country> _countryRepository;
        IGenericRepository<BloodType> _bloodTypeRepository;
        IGenericRepository<ExceptionRecord> _exceptionRepository;
        public PatientService(IGenericRepository<Patient> patientRepository,
                              IGenericRepository<Country> countryRepository,
                              IGenericRepository<BloodType> bloodTypeRepository,
                              IGenericRepository<ExceptionRecord> exceptionRepository)
        {
            _patientRepository = patientRepository ?? throw new ArgumentNullException(nameof(patientRepository));
            _countryRepository = countryRepository ?? throw new ArgumentNullException(nameof(countryRepository));
            _bloodTypeRepository = bloodTypeRepository ?? throw new ArgumentNullException(nameof(bloodTypeRepository));
            _exceptionRepository = exceptionRepository ?? throw new ArgumentNullException(nameof(exceptionRepository));
        }
        public void RegisterException(Exception e)
        {
            ExceptionRecord ex = Mapper.Map<ExceptionRecord>(e);
            _exceptionRepository.Insert(ex);
        }
        #region IpatientServiceMembers
        public void CreatePatient(Patient patient)
        {
            try
            {
                _patientRepository.Insert(patient);
                _patientRepository.UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }
        public Patient GetPatient(Guid ID)
        {
            try
            {
                return _patientRepository.Get(x=> x.ID == ID,null,"Nationality,BloodType").FirstOrDefault();
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }

        public IEnumerable<Patient> GetPatients()
        {
            try
            {
                return _patientRepository.GetAll();
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }
        public void UpdatePatient(Patient patient)
        {
            try
            {
                _patientRepository.Update(patient);
                _patientRepository.UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }
        public void DeletePatient(Patient patient)
        {
            try
            {
                _patientRepository.Delete(patient);
                _patientRepository.UnitOfWork.Commit();
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }

        public IEnumerable<Country> GetCountries()
        {
            return _countryRepository.GetAll();
        }

        public Country GetCountry(string code)
        {
            try
            {
                return _countryRepository.GetFirst(x => x.Code == code);
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }

        public IEnumerable<Object> GetBloodTypes()
        {
            try
            {
                var bloodTypes = _bloodTypeRepository.GetAll();
                var query = from c in bloodTypes select new { ID = c.ID, Caption = c.Type + c.RH };
                return query;
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }

        public BloodType GetBloodType(Guid ID)
        {
            try
            {
                return _bloodTypeRepository.GetById(ID);
            }
            catch (Exception e)
            {
                RegisterException(e);
                throw e;
            }
        }
        #endregion
    }
}
