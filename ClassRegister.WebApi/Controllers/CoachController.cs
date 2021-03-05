using ClassRegister.BusinessLayer.Services;
using ClassRegister.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ClassRegister.WebApi.Controllers
{
    [Route("api/coaches")]
    public class CoachController : ControllerBase
    {
        private ICoachService _coachService;

        public CoachController(ICoachService coachService)
        {
            _coachService = coachService;
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
    }
}
