using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Entity
{
    public class CourseEntity
    {

        public int IdCourse { get; set; }

        public string Description { get; set; }

        public ICollection<StudentEntity> Students { get; set; }

        public ICollection<SubjectEntity> Subjects { get; set; }
        public int NumberOfTeachers { get; set; }
    }
}