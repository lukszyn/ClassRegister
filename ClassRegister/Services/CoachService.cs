using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;

namespace ClassRegister.BusinessLayer.Services
{
    public interface ICoachService
    {
        void Add(Coach coach);
    }

    public class CoachService : ICoachService
    {
        private Func<IClassRegisterDbContext> _classRegisterFactoryMethod;

        public CoachService(Func<IClassRegisterDbContext> classRegisterFactoryMethod)
        {
            _classRegisterFactoryMethod = classRegisterFactoryMethod;
        }

        public void Add(Coach coach)
        {
            using (var context = _classRegisterFactoryMethod())
            {
                context.Coaches.Add(coach);
                context.SaveChanges();
            }
        }
    }
}
