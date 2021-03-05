using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRegister.BusinessLayer.Services
{
    public interface ICoursesService
    {
        void Add(Course course);
    }

    public class CoursesService : ICoursesService
    {
        private readonly Func<IClassRegisterDbContext> _classRegisterDbContextFactoryMethod;

        public CoursesService(Func<IClassRegisterDbContext> classRegisterDbContextFactoryMethod)
        {
            _classRegisterDbContextFactoryMethod = classRegisterDbContextFactoryMethod;
        }

        public void Add(Course course)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                context.Students.AttachRange(course.Students);
                context.Coaches.Attach(course.Coach);
                context.Courses.Add(course);
                context.SaveChanges();
            }
        }
    }
}
