using ClassRegister.DataLayer;
using System;

namespace ClassRegister.Services
{
    public interface IDbManagementService
    {
        void EnsureDatabaseCreation();
    }

    public class DbManagementService : IDbManagementService
    {
        private Func<IClassRegisterDbContext> _classRegisterDbContextFactoryMethod;

        public DbManagementService(Func<IClassRegisterDbContext> classRegisterDbContextFactoryMethod)
        {
            _classRegisterDbContextFactoryMethod = classRegisterDbContextFactoryMethod;
        }

        public void EnsureDatabaseCreation()
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                context.Database.EnsureCreated();
            }
        }
    }
}
