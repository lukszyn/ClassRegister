using System;
using System.Collections.Generic;

namespace ClassRegister.Admin.Models
{
    public enum Status
    {
        Active = 0,
        Completed = 1,
    }

    public class Course
    {
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Coach Coach { get; set; }
        public double AttendanceThreshold { get; set; } = 0.7;
        public double HomeworkThreshold { get; set; } = 0.7;
        public double TestThreshold { get; set; } = 0.7;
        public ICollection<Student> Students { get; set; }
        public Status Status { get; set; }
    }
}
