using ClassRegister.BusinessLayer.Services;
using ClassRegister.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;

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
        public void PostCoach([FromBody] Coach coach)
        {
            _coachService.Add(coach);
        }
    }
}
