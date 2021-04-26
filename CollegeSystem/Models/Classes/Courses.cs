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
    public class Courses : IGeneral
    {
        int _idCourse { get; set; }
        [Key]
        public int IdCourse
        {
            get
            {
                return _idCourse;
            }
            set
            {
                _idCourse = value;
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

        ICollection<Registration> _registrations { get; set; }
        public virtual ICollection<Registration> Registrations
        {
            get
            {
                return _registrations;
            }
            set
            {
                _registrations = value;
            }
        }
        ICollection<Subjects> _subjects { get; set; }
        public ICollection<Subjects> Subjects
        {
            get
            {
                return _subjects;
            }
            set
            {
                _subjects = value;
            }
        }

        public Courses()
        {
            this.Registrations = new HashSet<Registration>();
            this.Subjects = new HashSet<Subjects>();
        }
        public Courses(int? Id)
        {
            _idCourse = (Id.HasValue ? Id.Value : 0);
            this.Registrations = new HashSet<Registration>();
            this.Subjects = new HashSet<Subjects>();
        }

        public Courses(Courses c)
        {
            IdCourse = c.IdCourse;
            Description = c.Description;
            Subjects = c.Subjects;
            this.Registrations = new HashSet<Registration>();
            this.Subjects = new HashSet<Subjects>();
        }

        public bool Save()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Courses CourseFound = db.Courses.Find(_idCourse);
            if (CourseFound == null)
            {
                db.Courses.Add(this);
            }
            else
            {
                CourseFound.Description = _description;
            }

            db.SaveChanges();

            return true;
        }

        public bool Remove()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Courses Course = db.Courses.Find(_idCourse);
            db.Courses.Remove(Course);
            db.SaveChanges();

            return true;
        }

        public object GetData()
        {
            CollegeSystemContext db = new CollegeSystemContext();
            
            var course = (from a in db.Courses
                          select new CourseEntity
                          {
                              IdCourse = a.IdCourse,
                              Description = a.Description,
                              Students = (from x in a.Registrations
                                          where x.IdCourse == a.IdCourse
                                          select new StudentEntity
                                          {
                                              IdStudent = x.Students.IdStudent,
                                              Birthday = x.Students.Birthday,
                                              Name = x.Students.Name,
                                              RegistrationNumber = x.Students.RegistrationNumber,
                                              AverageGrades = (from av in db.Grades
                                                               where av.IdStudent == x.Students.IdStudent
                                                               && a.Subjects.Select(sub=>sub.IdSubject).Contains(av.IdSubject)
                                                               select av.Grade).Average()

                                          }).ToList(),
                              Subjects = (from x in a.Subjects
                                          select new SubjectEntity
                                          {
                                              IdSubject = x.IdSubject,
                                              Description = x.Description,
                                              Grades = (from g in x.Grades
                                                        select new GradeEntity
                                                        {
                                                            Grade = g.Grade,
                                                            IdGrade = g.IdGrade
                                                        }
                                                       ).ToList(),
                                              Teacher = new TeacherEntity
                                              {
                                                  Birthday = x.Teacher.Birthday,
                                                  IdTeacher = x.Teacher.IdTeacher,
                                                  Name = x.Teacher.Name,
                                                  Salary = x.Teacher.Salary
                                              }

                                          }).ToList()

                          });

            if (_idCourse > 0)
            {
                course = (from a in course
                          where a.IdCourse == _idCourse
                          select a);
            }

            var co = course.ToList();

            //Set the NumberOfTeachers with number of teachers registered form each course
            co.ForEach(a=>a.NumberOfTeachers = a.Subjects.Select(t=>t.Teacher).Count());

            return co.ToList();
        }
    }
}