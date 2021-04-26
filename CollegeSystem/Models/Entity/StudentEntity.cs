using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Entity
{
    public class StudentEntity
    {
        public int IdStudent { get; set; }
        public string Name { get; set; }
        public DateTime Birthday { get; set; }
        public long RegistrationNumber { get; set; }
        public ICollection<CourseEntity> Courses { get; set; }
        public decimal? AverageGrades { get; set; }
        public List<SubjectEntity> Subjects { get; set; }
    }
}