using System;
using System.Collections.Generic;

namespace ClassRegister.Admin.Models
{
    public class Course
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public Coach Coach { get; set; }
        public int AttendanceThreshold { get; set; } = 70;
        public int HomeworkThreshold { get; set; } = 70;
        public int TestThreshold { get; set; } = 70;
        public ICollection<Student> Students { get; set; }
    }
}
