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
    public class Registration : IGeneral
    {
        int _idRegistration { get; set; }
        [Key]
        public int IdRegistration
        {
            get
            {
                return _idRegistration;
            }
            set
            {
                _idRegistration = value;
            }
        }

        public int IdStudent { get; set; }
        public int IdCourse { get; set; }
        public Student Students { get; set; }
        public Courses Course { get; set; }
        public Registration()
        {

        }

        public Registration(int idStudent, int idCourse)
        {
            IdStudent = idStudent;
            IdCourse = idCourse;

        }

        public Registration(int? Id)
        {
            IdRegistration = (Id.HasValue ? Id.Value : 0);

        }

        public Registration(Registration registration)
        {
            IdRegistration = registration.IdRegistration;
            IdStudent = registration.IdStudent;
            IdCourse = registration.IdCourse;
        }

        public bool Save()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Registration RegistrationFound = db.Registrations.Find(_idRegistration);
            if (RegistrationFound == null)
            {
                db.Registrations.Add(this);
                db.SaveChanges();
                return true;
            }
            else
            {
                RegistrationFound.IdCourse = IdCourse;
                RegistrationFound.IdStudent = IdStudent;

                db.SaveChanges();

                return true;
            }
        }

        public bool Remove()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Registration Registration = db.Registrations.Find(IdRegistration);
            db.Registrations.Remove(Registration);
            db.SaveChanges();

            return true;
        }

        public object GetData()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            var registration = (from a in db.Registrations
                                select new RegistrationEntity
                                {
                                    IdRegistration = a.IdRegistration,
                                    Course = new CourseEntity
                                    {
                                        IdCourse = a.Course.IdCourse,
                                        Description = a.Course.Description
                                    },
                                    Student = new StudentEntity
                                    {
                                        IdStudent = a.Students.IdStudent,
                                        Birthday = a.Students.Birthday,
                                        Name = a.Students.Name,
                                        RegistrationNumber = a.Students.RegistrationNumber
                                    }

                                });

            if (_idRegistration > 0)
            {
                registration = (from a in registration
                                where a.IdRegistration == _idRegistration
                                select a);
            }

            return registration.ToList();

        }
    }
}