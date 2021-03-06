using ClassRegister.BusinessLayer.Services;
using ClassRegister.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ClassRegister.WebApi.Controllers
{
    [Route("api/coaches")]
    public class CoachController : ControllerBase
    {
        private ICoachService _coachService;
        private ICoursesService _coursesService;

        public CoachController(
            ICoachService coachService,
            ICoursesService coursesService)
        {
            _coachService = coachService;
            _coursesService = coursesService;
        }

        [HttpPost]
        public StatusCodeResult PostCoach([FromBody] Coach coach)
        {
            try
            {
                _coachService.Add(coach);
                return new StatusCodeResult(200);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }

        }

        [HttpGet("{coachId}/courses")]
        public List<Course> GetCourses(int coachId)
        {
            return _coursesService.GetActiveCourses(coachId);
        }

        [HttpGet]
        [Route("credentials")]
        public Coach LoginCoach([FromQuery] Credentials credentials)
        {
            return _coachService.Login(credentials);
        }

        [HttpGet]
        [Route("{email}")]
        public Coach GetCoach(string email)
        {
            return _coachService.GetCoach(email);
        }
    }
}
