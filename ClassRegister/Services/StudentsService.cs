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
                return context.Students.Include(s => s.Course).Where(s => s.Course.Id == courseId).ToList();
            }
        }
    }
}
