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
        public StatusCodeResult PostStudent([FromBody] Student student)
        {
            if (!_studentsService.CheckIfStudentExists(student))
            {
                _studentsService.Add(student);
                return new StatusCodeResult(200);
            } else
            {
                return new BadRequestResult();
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