using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassRegister.DataLayer.Models
{
    public enum State
    {
        Active = 0,
        Completed = 1,
    }

    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public Coach Coach { get; set; }
        public double AttendanceThreshold { get; set; } = 0.7;
        public double HomeworkThreshold { get; set; } = 0.7;
        public double TestThreshold { get; set; } = 0.7;
        public ICollection<Student> Students { get; set; }
        public State State { get; set; }
    }
}
