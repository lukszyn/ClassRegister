using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;
using System.Linq;
using System.Net;

namespace ClassRegister.BusinessLayer.Services
{
    public interface ICoachService
    {
        void Add(Coach coach);
        Coach Login(Credentials credentials);
        Coach GetCoach(string email);
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

        public Coach Login(Credentials credentials)
        {
            using (var context = _classRegisterFactoryMethod())
            {
                return context.Coaches.FirstOrDefault(c => c.Email == credentials.Email
                    && c.Password == credentials.Password);
            }
        }

        public Coach GetCoach(string email)
        {
            using (var context = _classRegisterFactoryMethod())
            {
                return context.Coaches.FirstOrDefault(c => c.Email == email);
            }
        }
    }
}
