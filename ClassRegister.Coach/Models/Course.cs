using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassRegister.CoachApp.Models
{
    public enum State
    {
        Active = 0,
        Completed = 1,
    }

    public class Course
    {
        public static int AttendanceThresholdDefaultValue => 70;
        public static int HomeworkThresholdDefaultValue => 70;
        public static int TestThresholdDefaultValue => 70;


        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        [Required]
        public Coach Coach { get; set; }
        public int AttendanceThreshold { get; set; }
        public int HomeworkThreshold { get; set; }
        public int TestThreshold { get; set; }
        public ICollection<Student> Students { get; set; }
        public State State { get; set; } = State.Active;
    }
}