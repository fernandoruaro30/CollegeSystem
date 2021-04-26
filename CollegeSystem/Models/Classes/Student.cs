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
    public class Student : Person
    {
        int _idStudent { get; set; }
        [Key]
        public int IdStudent
        {
            get
            {
                return _idStudent;
            }
            set
            {
                _idStudent = value;
            }
        }

        long _registrationNumber { get; set; }
        public long RegistrationNumber
        {
            get
            {
                return _registrationNumber;
            }
            set
            {
                _registrationNumber = value;
            }
        }

        ICollection<Registration> _registrations { get; set; }
        public ICollection<Registration> Registrations
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

        public Student() : base()
        {
            this.Registrations = new HashSet<Registration>();
        }

        public Student(Student s) : base(s)
        {
            IdStudent = s.IdStudent;
            Name = s.Name;
            RegistrationNumber = s.RegistrationNumber;
            this.Registrations = new HashSet<Registration>();
        }
        public Student(int? Id)
        {
            _idStudent = (Id.HasValue ? Id.Value : 0);
            this.Registrations = new HashSet<Registration>();
        }

        public override bool Save()
        {

            CollegeSystemContext db = new CollegeSystemContext();

            Student StudentFound = db.Students.Find(_idStudent);
            if (StudentFound == null)
            {
                db.Students.Add(this);
            }
            else
            {
                StudentFound.Name = Name;
                StudentFound.RegistrationNumber = RegistrationNumber;
                StudentFound.Birthday = Birthday;
            }

            db.SaveChanges();

            return true;
        }

        public override bool Remove()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Student student = db.Students.Find(_idStudent);
            db.Students.Remove(student);
            db.SaveChanges();

            return true;
        }
        public override object GetData()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            var students = (from a in db.Students
                            select new StudentEntity
                            {
                                IdStudent = a.IdStudent,
                                Birthday = a.Birthday,
                                Name = a.Name,
                                RegistrationNumber = a.RegistrationNumber,
                                Subjects = (from sub in a.Registrations.Select(y=>y.Course).FirstOrDefault().Subjects
                                            select new SubjectEntity
                                            {
                                                IdSubject = sub.IdSubject,
                                                Description = sub.Description,
                                                Grades = (from g in sub.Grades
                                                          where g.IdStudent == a.IdStudent
                                                          select new GradeEntity
                                                          {
                                                              IdGrade = g.IdGrade,
                                                              Grade = g.Grade
                                                          }).ToList()
                                            }).ToList()
                            });

            if (_idStudent > 0)
            {
                students = (from a in students
                            where a.IdStudent == _idStudent
                            select a);
            }

            return students.ToList();
        }
    }
}