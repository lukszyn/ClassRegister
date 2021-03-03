using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;

namespace ClassRegister.BusinessLayer.Services
{
    public interface IStudentsService
    {
        void Add(Student student);
    }

    public class StudentsService : IStudentsService
    {
        private readonly Func<IClassRegisterDbContext> _classRegisterDbContextFactoryMethod;

        public StudentsService(Func<IClassRegisterDbContext> classRegisterDbContextFactoryMethod)
        {
            _classRegisterDbContextFactoryMethod = classRegisterDbContextFactoryMethod;
        }

        public void Add(Student student)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                context.Students.Add(student);
                context.SaveChanges();
            }
        }
    }
}
