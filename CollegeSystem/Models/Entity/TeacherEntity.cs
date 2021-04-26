using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Entity
{
    public class TeacherEntity
    {
        public int IdTeacher { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public decimal Salary { get; set; }
        public ICollection<Subjects> Subjects { get; set; }
    }
}