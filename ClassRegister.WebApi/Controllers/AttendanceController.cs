using ClassRegister.BusinessLayer.Services;
using ClassRegister.DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ClassRegister.WebApi.Controllers
{
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private IAttendanceService _attendanceService;
        private ICoursesService _coursesService;

        public AttendanceController(
            IAttendanceService attendanceService,
            ICoursesService coursesService)
        {
            _attendanceService = attendanceService;
            _coursesService = coursesService;
        }

        [HttpPost]
        public StatusCodeResult PostAttendance([FromBody] Attendance attendance)
        {
            try
            {
                _attendanceService.Add(attendance);
                return new StatusCodeResult(200);
            }
            catch (Exception)
            {
                return new BadRequestResult();
            }

        }
    }
}
