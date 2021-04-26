using CollegeSystem.Models.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Entity
{
    public class GradeEntity
    {
        public int IdGrade { get; set; }

        public decimal Grade { get; set; }

        public SubjectEntity Subject { get; set; }
        public StudentEntity Student { get; set; }
        public int IdStudent { get; set; }
        public int IdSubject { get; set; }
    }
}