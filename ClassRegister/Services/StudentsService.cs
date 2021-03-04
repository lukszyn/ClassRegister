﻿using ClassRegister.DataLayer;
using ClassRegister.DataLayer.Models;
using System;
using System.Linq;

namespace ClassRegister.BusinessLayer.Services
{
    public interface IStudentsService
    {
        void Add(Student student);
        Student GetStudent(string email);
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

        public Student GetStudent(string email)
        {
            using (var context = _classRegisterDbContextFactoryMethod())
            {
                return context.Students.FirstOrDefault(s => s.Email == email);
            }
        }
    }
}
