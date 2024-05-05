using Microsoft.AspNetCore.Mvc;
using Serilog;
using UPB.BusinessLogic.Managers;
using UPB.BusinessLogic.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NewAppVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private PatientManager _patientManager;

        public PatientsController()
        {
            _patientManager = new PatientManager();
        }

        // GET: api/<PatientsController>
        [HttpGet]
        public List<Patient> Get()
        {
            return _patientManager.GetAll();
        }

        // GET api/<PatientsController>/5
        [HttpGet("{ci}")]
        public string Get(int ci)
        {
            Patient foundPatient = _patientManager.GetPatientByCI(ci);
            return foundPatient.GetInfo();
        }

        // POST api/<PatientsController>
        [HttpPost]
        public void Post([FromBody] Patient patient)
        {
            _patientManager.CreatePatient(patient.name, patient.lastName, patient.ci);
            Log.Information($"Patient created successfully with ci {patient.ci}");
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{ci}")]
        public void Put(int ci, [FromBody] Patient newPatientValues)
        {
            _patientManager.UpdatePatient(ci, newPatientValues);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{ci}")]
        public void Delete(int ci)
        {
            _patientManager.DeletePatient(ci);
        }
    }
}
