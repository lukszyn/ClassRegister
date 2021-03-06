using Newtonsoft.Json;
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
        public int AttendanceThreshold { get; set; } = 70;
        public int HomeworkThreshold { get; set; } = 70;
        public int TestThreshold { get; set; } = 70;
        public ICollection<Student> Students { get; set; }
        public State State { get; set; } = State.Active;
    }
}
