using Microsoft.AspNetCore.Mvc;
using UPB.BusinessLogic.Managers;
using UPB.BusinessLogic.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UPB.NewAppVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private StudentManager _studentManager;

        public StudentController(StudentManager studentManager)
        {
            _studentManager = studentManager;
        }

        // GET: api/<StudentController>
        [HttpGet]
        public List<Student> Get()
        {
            return _studentManager.GetAll();
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            Task<Student> studentFoundTask = _studentManager.GetStudentByID(id);
            return studentFoundTask.Result != null ? studentFoundTask.Result : null;
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] Student value)
        {
            _studentManager.CreateStudent(value);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Student value)
        {
            _studentManager.UpdateStudent(id, value);
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _studentManager.DeleteStudent(id);
        }
    }
}
