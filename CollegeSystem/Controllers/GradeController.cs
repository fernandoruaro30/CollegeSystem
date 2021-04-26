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
    public class GradeController : Controller
    {
        // GET: Grade
        public ActionResult Index()
        {
            return View();
        }

        // GET: Grade/GetGrade
        [HttpGet]
        public JsonResult GetGrade(int? IdGrade)
        {
            Grades grade = new Grades(IdGrade);
            List<GradeEntity> lstGrades = (List<GradeEntity>)grade.GetData();
            
            return new JsonResult { Data = lstGrades, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Grade/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grade/Save
        [HttpPost]
        public JsonResult Save(Grades grade1)
        {
            try
            {
                Grades grade = new Grades(grade1);
                var ret = grade.Save();

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
        public ActionResult Edit(int? IdGrade)
        {
            Grades Grade = new Grades(IdGrade);
            List<GradeEntity> grades = (List<GradeEntity>)Grade.GetData();

            return View(grades[0]);
        }

        // POST: Grade/Delete
        public JsonResult Delete(int? IdGrade)
        {
            Grades Grade = new Grades(IdGrade);
            var ret = Grade.Remove();

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
