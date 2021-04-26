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
using Newtonsoft.Json;

namespace CollegeSystem.Controllers
{
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }

        // GET: Teacher/GetTeacher
        [HttpGet]
        public JsonResult GetTeacher(int? IdTeacher)
        {
            Teacher teacher = new Teacher(IdTeacher);
            List<TeacherEntity> lstTeachers = (List<TeacherEntity>)teacher.GetData();

            return new JsonResult { Data = lstTeachers, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Teacher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Teacher/Save
        [HttpPost]
        public JsonResult Save(Teacher Teacher)
        {
            try
            {
                Teacher teacher = new Teacher(Teacher);
                var ret = teacher.Save();

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
        public ActionResult Edit(int? IdTeacher)
        {
            Teacher teacher = new Teacher(IdTeacher);
            List<TeacherEntity> lstTeachers = (List<TeacherEntity>)teacher.GetData();

            return View(lstTeachers[0]);
        }

        // POST: Teacher/Delete
        public JsonResult Delete(int? IdTeacher)
        {
            Teacher teacher = new Teacher(IdTeacher);
            var ret = teacher.Remove();

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
