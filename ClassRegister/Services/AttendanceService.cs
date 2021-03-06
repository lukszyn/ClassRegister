using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;
using System.Linq;
using System.Net;

namespace ClassRegister.BusinessLayer.Services
{
    public interface IAttendanceService
    {
        void Add(Attendance attendance);
    }

    public class AttendanceService : IAttendanceService
    {
        private Func<IClassRegisterDbContext> _classRegisterFactoryMethod;
        private IValidationService _validationService;

        public AttendanceService(
            Func<IClassRegisterDbContext> classRegisterFactoryMethod,
            IValidationService validationService)
        {
            _classRegisterFactoryMethod = classRegisterFactoryMethod;
            _validationService = validationService;
        }

        public void Add(Attendance attendance)
        {
            using (var context = _classRegisterFactoryMethod())
            {
                context.Students.Attach(attendance.Student);
                context.Attendances.Add(attendance);
                context.SaveChanges();
            }
        }
    }
}
