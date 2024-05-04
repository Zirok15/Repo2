using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UPB.NewAppVS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private List<string> _students;

        public StudentController()
        {
            _students = new List<string>();

            _students.Add("John");
            _students.Add("Jose");
            _students.Add("Maria");
        }

        // GET: api/<StudentController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _students;
        }

        // GET api/<StudentController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return _students[id];
        }

        // POST api/<StudentController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _students.Add(value);
        }

        // PUT api/<StudentController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
            _students[id] = value;
        }

        // DELETE api/<StudentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _students.RemoveRange(id, 1);
        }
    }
}
