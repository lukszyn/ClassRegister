using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ClassRegister.BusinessLayer.Services
{
    public interface IStudentsService
    {
        void Add(Student student);
        bool CheckIfStudentExists(Student student);
        Student GetStudent(string email);
        List<Student> GetStudents(int courseId);
        void Update(Student student);
        public List<Student> GetStudentsFromCourse(int courseId);
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

        public bool CheckIfStudentExists(Student student)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                return context.Students.Any(s => s.Email == student.Email);
            }
        }

        public Student GetStudent(string email)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                return context.Students.FirstOrDefault(s => s.Email == email);
            }
        }

        public List<Student> GetStudents(int courseId)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                return context.Courses
                    .Include(c => c.Students)
                    .FirstOrDefault(c => c.Id == courseId)
                    .Students.ToList();
            }
        }

        public List<Student> GetStudentsFromCourse(int courseId)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                return context.Students
                    .Where(c => c.Course.Id==courseId)
                    .ToList();
            }
        }

        public void Update(Student student)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                context.Students.Update(student);
            }
        }
    }
}
