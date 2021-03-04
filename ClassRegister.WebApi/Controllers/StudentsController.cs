using ClassRegister.BusinessLayer.Services;
using ClassRegister.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegister.WebApi.Controllers
{
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentsService _studentsService;

        public StudentsController(IStudentsService studentsService)
        {
            _studentsService = studentsService;
        }

        [HttpPost]
        public string PostStudent([FromBody] Student student)
        {
            if (!_studentsService.CheckIfStudentExists(student))
            {
                _studentsService.Add(student);
                return "Student added successfully";
            } else
            {
                return "Something went wrong, try again.";
            }
        }

        [HttpGet]
        [Route("{email}")]
        public Student GetStudent(string email)
        {
            return _studentsService.GetStudent(email);
        }
    }
}