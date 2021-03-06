using System;
using System.Collections.Generic;

namespace ClassRegister.CoachApp.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public Course Course { get; set; }
        public ICollection<Attendance> Attendances { get; set; }
    }
}