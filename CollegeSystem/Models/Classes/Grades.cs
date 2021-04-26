using CollegeSystem.DataBase;
using CollegeSystem.Models.Entity;
using CollegeSystem.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Classes
{
    public class Grades : IGeneral
    {
        int _idGrade { get; set; }
        [Key]
        public int IdGrade
        {
            get
            {
                return _idGrade;
            }
            set
            {
                _idGrade = value;
            }
        }

        decimal _grade { get; set; }
        public decimal Grade
        {
            get
            {
                return _grade;
            }
            set
            {
                _grade = value;
            }
        }
        
        public int IdSubject { get; set; }
        public int IdStudent { get; set; }

        public Grades()
        {

        }

        public Grades(Grades g)
        {
            IdGrade = g.IdGrade;
            Grade = g.Grade;
            IdStudent = g.IdStudent;
            IdSubject = g.IdSubject;
        }

        public Grades(int? Id)
        {
            _idGrade = (Id.HasValue ? Id.Value : 0);
        }

        public bool Save()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Grades GradeFound = db.Grades.Find(_idGrade);

            if (GradeFound == null)
            {
                db.Grades.Add(this);
                db.SaveChanges();

                return true;
            }
            else
            {
                GradeFound.Grade = _grade;
                GradeFound.IdStudent = IdStudent;
                GradeFound.IdSubject = IdSubject;

                db.SaveChanges();

                return true;
            }
        }

        public bool Remove()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Grades Grade = db.Grades.Find(IdGrade);
            db.Grades.Remove(Grade);
            db.SaveChanges();

            return true;
        }

        public object GetData()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            var grade = (from a in db.Grades
                         select new GradeEntity
                         {
                             IdGrade = a.IdGrade,
                             Grade = a.Grade,
                             IdStudent = a.IdStudent,
                             IdSubject = a.IdSubject,
                             Student = (from x in db.Students
                                        where x.IdStudent == a.IdStudent
                                        select new StudentEntity
                                         {
                                             IdStudent = x.IdStudent,
                                             Birthday = x.Birthday,
                                             Name = x.Name,
                                             RegistrationNumber = x.RegistrationNumber
                                         }).FirstOrDefault(),
                             Subject = (from x in db.Subjects
                                        where x.IdSubject == a.IdSubject
                                        select new SubjectEntity
                                        {
                                            IdSubject = x.IdSubject,
                                            Description = x.Description
                                            
                                        }).FirstOrDefault()
                         });

            if (_idGrade > 0)
            {
                grade = (from a in grade
                         where a.IdGrade == _idGrade
                         select a);
            }

            return grade.ToList();
        }
    }
}