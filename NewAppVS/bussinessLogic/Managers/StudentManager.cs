using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPB.BusinessLogic.Managers.Exceptions;
using UPB.BusinessLogic.Models;

namespace UPB.BusinessLogic.Managers
{
    public class StudentManager
    {
        private List<Student> _students;
        private readonly IConfiguration _configuration;

        public StudentManager(IConfiguration configuration)
        {
            _students = new List<Student>();
            _configuration = configuration;

            _students.Add(new Student()
            {
                Name = "Sebastian",
                Code = "68192",
                Id = 1
            });
            _students.Add(new Student()
            {
                Name = "Katya",
                Code = "68207",
                Id = 2
            });

        }

        public List<Student> GetAll()
        {
            return _students;
        }

        public Student CreateStudent(Student student)
        {
            Student createdStudent = new Student()
            {
                Name = student.Name,
                Code = student.Code,
                Id = student.Id
            };
            _students.Add(createdStudent);
            return createdStudent;
        }

        public Student UpdateStudent(int id, Student studentToUpdate)
        {
            // logica para buscar y actualizar
            return studentToUpdate;
        }

        public Student DeleteStudent(int studentIdToDelete)
        {
            // logica de buscar y eliminar
            throw new NotImplementedException();
        }

        public async Task<Student> GetStudentByID(int id)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client.GetAsync("https://api.restfuleeeee-api.dev/objects/7");
                string responseBody = await response.Content.ReadAsStringAsync();
                Log.Information(responseBody);

                Student foundStudent = _students.Find(x => x.Id == id);
                return foundStudent;
            }
            catch(Exception ex)
            {
                BackingServiceException exception = new BackingServiceException(ex.Message);
                Log.Error(exception.GetMessageForLogs("GetStudentByID"));
                Log.Error("Stack Trace: " + ex.StackTrace);
                throw exception;
            }
        }
    }
}
