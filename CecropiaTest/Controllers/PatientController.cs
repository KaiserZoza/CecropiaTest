using Entities;
using Logic;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace CecropiaTest.Controllers
{
    public class PatientController : ApiController
    {
        private readonly IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new System.ArgumentNullException(nameof(patientService));
        }
        // GET: api/Patient
        public IEnumerable<Patient> Get()
        {
            return _patientService.GetPatients();
        }

        // GET: api/Patient/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Patient
        public bool Post(Patient patient)
        {
            try
            {
                _patientService.CreatePatient(patient);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // PUT: api/Patient/5
        public bool Put(Patient patient)
        {
            try
            {
                _patientService.UpdatePatient(patient);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // DELETE: api/Patient/5
        public bool Delete(Guid id)
        {
            try
            {
                var patient = _patientService.GetPatient(id);
                _patientService.DeletePatient(patient);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
