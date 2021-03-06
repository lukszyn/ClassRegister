﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ClassRegister.Admin.Models
{
    public class Coach
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Course> Courses { get; set; }
    }
}
