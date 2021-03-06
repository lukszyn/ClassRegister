using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassRegister.BusinessLayer.Services
{
    public interface ICoursesService
    {
        void Add(Course course);
        List<Course> GetActiveCourses(int coachId);
        Course GetCoursById(int courseId);
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

        public Course GetCoursById(int courseId)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                return context.Courses
                    .FirstOrDefault(x => x.Id == courseId);
            }
        }

        public List<Course> GetActiveCourses(int coachId)
        {
            using (var contex = _classRegisterDbContextFactoryMethod())
            {
                return contex.Courses
                    .Where(x => x.Coach.Id == coachId && x.State == State.Active)
                    .ToList();
            }
        }
    }
}
