namespace CollegeSystem.Migrations
{
    using CollegeSystem.Models.Classes;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
     
    public sealed class Configuration : DropCreateDatabaseAlways<CollegeSystem.DataBase.CollegeSystemContext>
    { 
        protected override void Seed(CollegeSystem.DataBase.CollegeSystemContext context)
        {
            var students = new List<Student>
            {
                new Student{IdStudent=1,Name="Student 1",Birthday=DateTime.Parse("1985-09-01"),RegistrationNumber=1},
                new Student{IdStudent=2,Name="Student 2",Birthday=DateTime.Parse("1980-10-18"),RegistrationNumber=2},
                new Student{IdStudent=3,Name="Student 3",Birthday=DateTime.Parse("1990-01-01"),RegistrationNumber=3},
                new Student{IdStudent=4,Name="Student 4",Birthday=DateTime.Parse("1988-12-23"),RegistrationNumber=4},
                new Student{IdStudent=5,Name="Student 5",Birthday=DateTime.Parse("1995-03-18"),RegistrationNumber=5}
            };

            students.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            var teachers = new List<Teacher>
            {
                new Teacher{IdTeacher=1,Name="Teacher 1",Birthday=DateTime.Parse("1985-09-01"),Salary=decimal.Parse("1.900")},
                new Teacher{IdTeacher=2,Name="Teacher 2",Birthday=DateTime.Parse("1980-10-18"),Salary=decimal.Parse("2.335")},
                new Teacher{IdTeacher=3,Name="Teacher 3",Birthday=DateTime.Parse("1990-01-01"),Salary=decimal.Parse("2.000")},
                new Teacher{IdTeacher=4,Name="Teacher 4",Birthday=DateTime.Parse("1988-12-23"),Salary=decimal.Parse("1.850")},
                new Teacher{IdTeacher=5,Name="Teacher 5",Birthday=DateTime.Parse("1995-03-18"),Salary=decimal.Parse("1.450")},
                new Teacher{IdTeacher=6,Name="Teacher 6",Birthday=DateTime.Parse("1997-11-28"),Salary=decimal.Parse("2.010")},
                new Teacher{IdTeacher=7,Name="Teacher 7",Birthday=DateTime.Parse("1995-09-18"),Salary=decimal.Parse("2.300")},
                new Teacher{IdTeacher=8,Name="Teacher 8",Birthday=DateTime.Parse("1992-04-05"),Salary=decimal.Parse("1.596")},
                new Teacher{IdTeacher=9,Name="Teacher 9",Birthday=DateTime.Parse("1993-03-18"),Salary=decimal.Parse("1.745")},
                new Teacher{IdTeacher=10,Name="Teacher 10",Birthday=DateTime.Parse("1989-08-21"),Salary=decimal.Parse("1.698")}
            };

            teachers.ForEach(s => context.Teachers.Add(s));
            context.SaveChanges();

            var courses = new List<Courses>
            {
                new Courses{IdCourse=1,Description="Course 1"},
                new Courses{IdCourse=2,Description="Course 2"}
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var subjects = new List<Subjects>
            {
                new Subjects{IdSubject=1,Description="Subject 1",IdCourse = 1, IdTeacher = 1},
                new Subjects{IdSubject=2,Description="Subject 2",IdCourse = 1, IdTeacher = 2},
                new Subjects{IdSubject=3,Description="Subject 3",IdCourse = 1, IdTeacher = 3},
                new Subjects{IdSubject=4,Description="Subject 4",IdCourse = 1, IdTeacher = 4},
                new Subjects{IdSubject=5,Description="Subject 5",IdCourse = 1, IdTeacher = 5},
                new Subjects{IdSubject=6,Description="Subject 6",IdCourse = 2, IdTeacher = 6},
                new Subjects{IdSubject=7,Description="Subject 7",IdCourse = 2, IdTeacher = 7},
                new Subjects{IdSubject=8,Description="Subject 8",IdCourse = 2, IdTeacher = 8},
                new Subjects{IdSubject=9,Description="Subject 9",IdCourse = 2, IdTeacher = 9},
                new Subjects{IdSubject=10,Description="Subject 10",IdCourse = 2, IdTeacher = 10},

            };

            subjects.ForEach(s => context.Subjects.Add(s));
            context.SaveChanges();

            var registrations = new List<Registration>
            {
                new Registration{IdRegistration=1,IdStudent= 1,IdCourse = 1},
                new Registration{IdRegistration=2,IdStudent= 2,IdCourse = 1},
                new Registration{IdRegistration=3,IdStudent= 3,IdCourse = 1},
                new Registration{IdRegistration=4,IdStudent= 4,IdCourse = 2},
                new Registration{IdRegistration=5,IdStudent= 5,IdCourse = 2}

            };

            registrations.ForEach(s => context.Registrations.Add(s));
            context.SaveChanges();

            var grades = new List<Grades>
            {
                new Grades{IdGrade=1,IdStudent=1,IdSubject=1,Grade=decimal.Parse("5,5")},
                new Grades{IdGrade=2,IdStudent=1,IdSubject=2,Grade=decimal.Parse("7")},
                new Grades{IdGrade=3,IdStudent=1,IdSubject=3,Grade=decimal.Parse("8")},
                new Grades{IdGrade=4,IdStudent=1,IdSubject=4,Grade=decimal.Parse("4,5")},
                new Grades{IdGrade=5,IdStudent=1,IdSubject=5,Grade=decimal.Parse("5,0")},

                new Grades{IdGrade=6,IdStudent=2,IdSubject=1,Grade=decimal.Parse("5,9")},
                new Grades{IdGrade=7,IdStudent=2,IdSubject=2,Grade=decimal.Parse("7,2")},
                new Grades{IdGrade=8,IdStudent=2,IdSubject=3,Grade=decimal.Parse("4,3")},
                new Grades{IdGrade=9,IdStudent=2,IdSubject=4,Grade=decimal.Parse("9,3")},
                new Grades{IdGrade=10,IdStudent=2,IdSubject=5,Grade=decimal.Parse("10,0")},

                new Grades{IdGrade=11,IdStudent=3,IdSubject=1,Grade=decimal.Parse("6,4")},
                new Grades{IdGrade=12,IdStudent=3,IdSubject=2,Grade=decimal.Parse("7,8")},
                new Grades{IdGrade=13,IdStudent=3,IdSubject=3,Grade=decimal.Parse("10")},
                new Grades{IdGrade=14,IdStudent=3,IdSubject=4,Grade=decimal.Parse("10")},
                new Grades{IdGrade=15,IdStudent=3,IdSubject=5,Grade=decimal.Parse("7,0")},


                new Grades{IdGrade=16,IdStudent=4,IdSubject=6,Grade=decimal.Parse("10")},
                new Grades{IdGrade=17,IdStudent=4,IdSubject=7,Grade=decimal.Parse("10")},
                new Grades{IdGrade=18,IdStudent=4,IdSubject=8,Grade=decimal.Parse("5,2")},
                new Grades{IdGrade=19,IdStudent=4,IdSubject=9,Grade=decimal.Parse("4,5")},
                new Grades{IdGrade=20,IdStudent=4,IdSubject=10,Grade=decimal.Parse("5,0")},


                new Grades{IdGrade=21,IdStudent=5,IdSubject=6,Grade=decimal.Parse("3,5")},
                new Grades{IdGrade=22,IdStudent=5,IdSubject=7,Grade=decimal.Parse("7,1")},
                new Grades{IdGrade=23,IdStudent=5,IdSubject=8,Grade=decimal.Parse("8,9")},
                new Grades{IdGrade=24,IdStudent=5,IdSubject=9,Grade=decimal.Parse("9.8")},
                new Grades{IdGrade=25,IdStudent=5,IdSubject=10,Grade=decimal.Parse("8,1")},
            };

            grades.ForEach(s => context.Grades.Add(s));
            context.SaveChanges();
        }
    }
}
