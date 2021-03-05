using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRegister.DataLayer.Models
{
    public enum Status
    {
        Present = 1,
        Absent = 2,
        Justified = 3
    }
    public class Attendance
    {
        public int Id { get; set; }
        public DateTime ClassesDate { get; set; }
        public Student Student { get; set; }
        public Status Status { get; set; }
    }
}
