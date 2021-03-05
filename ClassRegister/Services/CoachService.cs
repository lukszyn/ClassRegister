using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;
using System.Net;

namespace ClassRegister.BusinessLayer.Services
{
    public interface ICoachService
    {
        void Add(Coach coach);
    }

    public class CoachService : ICoachService
    {
        private Func<IClassRegisterDbContext> _classRegisterFactoryMethod;
        private IValidationService _validationService;

        public CoachService(
            Func<IClassRegisterDbContext> classRegisterFactoryMethod,
            IValidationService validationService)
        {
            _classRegisterFactoryMethod = classRegisterFactoryMethod;
            _validationService = validationService;
        }

        public void Add(Coach coach)
        {
            if (_validationService.CheckFormatOfEnteredEmail(coach.Email) == true &&
                _validationService.CheckFormatOfEnteredPassword(coach.Password) == true)
            {
                using (var context = _classRegisterFactoryMethod())
                {
                    context.Coaches.Add(coach);
                    context.SaveChanges();
                }
            }
        }


    }
}
