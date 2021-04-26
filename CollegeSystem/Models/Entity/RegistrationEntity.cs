using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Entity
{
    public class RegistrationEntity
    {
        public int IdRegistration { get; set; }

        public StudentEntity Student { get; set; }
        public CourseEntity Course { get; set; }
    }
}