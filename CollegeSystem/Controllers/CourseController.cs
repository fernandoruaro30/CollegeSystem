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
    public class CourseController : Controller
    {

        // GET: Course
        public ActionResult Index()
        {
            return View();
        }

        // GET: Course/GetCourse
        [HttpGet]
        public JsonResult GetCourse(int? IdCourse)
        {
            Courses course = new Courses(IdCourse);
            List<CourseEntity> lstCourses = (List<CourseEntity>)course.GetData();

            return new JsonResult { Data = lstCourses, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Save
        [HttpPost]
        public JsonResult Save(Courses Course)
        {
            try
            {
                Courses course = new Courses(Course);
                var ret = course.Save();

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
        public ActionResult Edit(int? IdCourse)
        {
            Courses Course = new Courses(IdCourse);
            List<CourseEntity> lstCourses = (List<CourseEntity>)Course.GetData();

            return View(lstCourses[0]);
        }

        // POST: Course/Delete
        public JsonResult Delete(int? IdCourse)
        {
            Courses Course = new Courses(IdCourse);
            var ret = Course.Remove();

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
