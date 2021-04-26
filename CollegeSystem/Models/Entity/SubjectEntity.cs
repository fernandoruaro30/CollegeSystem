using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Entity
{
    public class SubjectEntity
    {
        public int IdSubject { get; set; }
        public string Description { get; set; }
        public TeacherEntity Teacher { get; set; }

        public List<GradeEntity> Grades { get; set; }
        public CourseEntity Course { get; set; }
        public int NumberOfStudents { get; set; }
    }
}