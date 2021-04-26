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
    public class Teacher : Person
    {
        int _idTeacher { get; set; }
        [Key]
        public int IdTeacher
        {
            get
            {
                return _idTeacher;
            }
            set
            {
                _idTeacher = value;
            }
        }
        decimal _salary { get; set; }
        public decimal Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                _salary = value;
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

        public Teacher() : base()
        {
            this.Subjects = new HashSet<Subjects>();
        }
        public Teacher(Teacher t) : base(t)
        {
            IdTeacher = t.IdTeacher;
            Name = t.Name;
            Salary = t.Salary;
            Subjects = t.Subjects;
            this.Subjects = new HashSet<Subjects>();
        }
        public Teacher(int? Id)
        {
            _idTeacher = (Id.HasValue ? Id.Value : 0);
            this.Subjects = new HashSet<Subjects>();
        }

        public override bool Save()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Teacher TeacherFound = db.Teachers.Find(_idTeacher);
            if (TeacherFound == null)
            {
                db.Teachers.Add(this);
            }
            else
            {
                TeacherFound.Name = Name;
                TeacherFound.Salary = Salary;
                TeacherFound.Birthday = Birthday;
            }

            db.SaveChanges();

            return true;
        }

        public override bool Remove()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            Teacher teacher = db.Teachers.Find(_idTeacher);
            db.Teachers.Remove(teacher);
            db.SaveChanges();

            return true;
        }
        public override object GetData()
        {
            CollegeSystemContext db = new CollegeSystemContext();

            var teachers = (from a in db.Teachers
                            select new TeacherEntity
                            {
                                IdTeacher = a.IdTeacher,
                                Birthday = a.Birthday,
                                Name = a.Name,
                                Salary = a.Salary

                            });

            if (_idTeacher > 0)
            {
                teachers = (from a in teachers
                            where a.IdTeacher == _idTeacher
                            select a);
            }

            return teachers.ToList();
        }
    }
}