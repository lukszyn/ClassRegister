using ClassRegister.BusinessLayer.Services;
using ClassRegister.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ClassRegister.WebApi.Controllers
{
    [Route("api/courses")]
    public class CoursesController : ControllerBase
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpPost]
        public void PostCourse([FromBody] Course course)
        {
            _coursesService.Add(course);
        }
    }
}
