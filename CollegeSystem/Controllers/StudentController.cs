using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CollegeSystem.DataBase;
using CollegeSystem.Models.Classes;
using CollegeSystem.Models.Entity;

namespace CollegeSystem.Controllers
{
    public class StudentController : Controller
    {

        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        // GET: Student/GetStudent
        [HttpGet]
        public JsonResult GetStudent(int? IdStudent)
        {
            Student student = new Student(IdStudent);
            List<StudentEntity> lstStudents = (List<StudentEntity>)student.GetData();

            return new JsonResult { Data = lstStudents, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Save
        [HttpPost]
        public JsonResult Save(Student student)
        {
            try
            {
                Student stud = new Student(student);
                var ret = stud.Save();

                return Json(new
                {
                    success = ret,
                    errors = ""
                });
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    errors = ex.ToString()
                });
            }
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int IdStudent)
        {
            Student student = new Student(IdStudent);
            List<StudentEntity> lstStudent = (List<StudentEntity>)student.GetData();

            return View(lstStudent[0]);
        }

        // POST: Student/Delete
        public JsonResult Delete(int IdStudent)
        {
            Student student = new Student(IdStudent);
            var ret = student.Remove();

            return new JsonResult
            {
                Data = new
                {
                    success = ret,
                    errors = ""

                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
