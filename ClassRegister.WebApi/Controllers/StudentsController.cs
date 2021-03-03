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
        public void PostStudent([FromBody] Student student)
        {
            _studentsService.Add(student);
        }

        [HttpGet]
        [Route("{email}")]
        public Student GetStudent(string email)
        {

        }
    }
}