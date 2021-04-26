using CollegeSystem.DataBase;
using CollegeSystem.Models.Entity;
using CollegeSystem.Models.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CollegeSystem.Models.Classes
{
    public class Subjects : IGeneral
    {
        int _idSubject { get; set; }
        [Key]
        public int IdSubject
        {
            get
            {
                return _idSubject;
            }
            set
            {
                _idSubject = value;
            }
        }

        string _description { get; set; }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public int IdTeacher { get; set; }
        [JsonIgnore]
        public virtual Teacher Teacher { get; set; }

        List<Grades> _grades { get; set; }
        public List<Grades> Grades
        {
            get
            {
                return _grades;
            }
            set
            {
                _grades = value;
            }
        }
        public int IdCourse { get; set; }
        [JsonIgnore]
        public virtual Courses Course { get; set; }

        public Subjects()
        {

        }

        public Subjects(Subjects s)
        {
            IdSubject = s.IdSubject;
            Description = s.Description;
            Teacher = s.Teacher;
            Course = s.Course;
            IdCourse = s.IdCourse;
            IdTeacher = s.IdTeacher;
            Grades = s.Grades;
        }

        public Subjects(int? Id)
        {
            _idSubject = (Id.HasValue ? Id.Value : 0);
        }

        public bool Save()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Subjects SubjectFound = db.Subjects.Find(_idSubject);
            if (SubjectFound == null)
            {
                db.Subjects.Add(this);
            }
            else
            {
                SubjectFound.Description = _description;
                SubjectFound.Teacher = Teacher;
                SubjectFound.Course = Course;
            }

            db.SaveChanges();

            return true;
        }

        public bool Remove()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Subjects subject = db.Subjects.Find(_idSubject);
            db.Subjects.Remove(subject);
            db.SaveChanges();

            return true;
        }

        public object GetData()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            var subject = (from a in db.Subjects
                           select new SubjectEntity
                           {
                               IdSubject = a.IdSubject,
                               Course = new CourseEntity
                               {
                                   Description = a.Course.Description,
                                   IdCourse = a.Course.IdCourse
                               },
                               Description = a.Description,
                               Teacher = new TeacherEntity
                               {
                                   Birthday = a.Teacher.Birthday,
                                   IdTeacher = a.Teacher.IdTeacher,
                                   Name = a.Teacher.Name,
                                   Salary = a.Teacher.Salary
                               },
                               Grades = (from g in a.Grades
                                         select new GradeEntity
                                         {
                                             Grade = g.Grade,
                                             IdGrade = g.IdGrade
                                         }
                               ).ToList(),
                               NumberOfStudents = a.Course.Registrations.Select(stu=>stu.IdStudent).Count()

                           });

            if (_idSubject > 0)
            {
                subject = (from a in subject
                           where a.IdSubject == _idSubject
                           select a);
            }

            return subject.ToList();
        }
    }
}