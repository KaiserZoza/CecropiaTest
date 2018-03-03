using AutoMapper;
using CecropiaWebApplication.Models;
using Entities;
using Logic;
using System;
using System.Net.Http;
using System.Web.Mvc;

namespace CecropiaWebApplication.Controllers
{
    public class PatientController : Controller
    {
        private IPatientService _patientService;
        public PatientController(IPatientService patientService)
        {
            _patientService = patientService ?? throw new ArgumentNullException(nameof(patientService));
        }

        // GET: Patient
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetPatients()
        {
            var patients = _patientService.GetPatients();
            return PartialView("_PatientList",patients);
        }
        [HttpGet]
        public ActionResult CreatePatient()
        {
            var countries = _patientService.GetCountries();
            var bloodTypes = _patientService.GetBloodTypes();
            return View(new PatientViewModel(countries,bloodTypes));
        }

        [HttpPost]
        public ActionResult CreatePatient(PatientViewModel viewModel)
        {
            Patient patient = Mapper.Map<Patient>(viewModel);
            Country nationality = _patientService.GetCountry(viewModel.SelectedNationality);
            patient.Nationality = nationality;
            BloodType bloodType = _patientService.GetBloodType(viewModel.SelectedBloodType);
            patient.BloodType = bloodType;
            patient.ID = Guid.NewGuid();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58888/api/Patient");
                var postTask = client.PostAsJsonAsync("patient", patient);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {   
                    return RedirectToAction("Index","Home",new { message = "Created!"});
                }
                ViewBag.Error = result.Content;
            }
            var countries = _patientService.GetCountries();
            viewModel.Countries = countries;
            var bloodTypes = _patientService.GetBloodTypes();
            viewModel.BloodTypes = bloodTypes;
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Edit(Guid id)
        {
            var patient = _patientService.GetPatient(id);
            var patientViewModel = Mapper.Map<PatientViewModel>(patient);
            var countries = _patientService.GetCountries();
            patientViewModel.Countries = countries;
            var bloodTypes = _patientService.GetBloodTypes();
            patientViewModel.BloodTypes = bloodTypes;
            return View(patientViewModel);
        }

        [HttpPost]
        public ActionResult Edit(PatientViewModel viewModel)
        {
            Patient patient = Mapper.Map<Patient>(viewModel);
            Country nationality = _patientService.GetCountry(viewModel.SelectedNationality);
            patient.Nationality = nationality;
            BloodType bloodType = _patientService.GetBloodType(viewModel.SelectedBloodType);
            patient.BloodType = bloodType;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58888/api/Patient/5");
                var postTask = client.PutAsJsonAsync("patient", patient);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home", new { message = "Edited!" });
                }
                ViewBag.Error = result.Content;
            }
            var countries = _patientService.GetCountries();
            viewModel.Countries = countries;
            var bloodTypes = _patientService.GetBloodTypes();
            viewModel.BloodTypes = bloodTypes;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Delete(Guid id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:58888/");
                var postTask = client.DeleteAsync("api/patient/" + id.ToString());
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                ViewBag.Error = result.Content;
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(Guid id)
        {
            var patient = _patientService.GetPatient(id);
            var patientViewModel = Mapper.Map<PatientDetailsViewModel>(patient);
            return View(patientViewModel);
        }
    }
}