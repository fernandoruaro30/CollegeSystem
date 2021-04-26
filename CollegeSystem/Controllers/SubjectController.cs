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
    public class SubjectController : Controller
    {
        // GET: Subject
        public ActionResult Index()
        {
            return View();
        }

        // GET: Subject/GetSubject
        [HttpGet]
        public JsonResult GetSubject(int? IdSubject)
        {
            Subjects subject = new Subjects(IdSubject);
            return new JsonResult { Data = subject.GetData(), JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        // GET: Subject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Subject/Save
        [HttpPost]
        public JsonResult Save(Subjects Subject)
        {
            try
            {
                Subjects subject = new Subjects(Subject);
                var ret = subject.Save();

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
        public ActionResult Edit(int? IdSubject)
        {
            Subjects subject = new Subjects(IdSubject);
            List<SubjectEntity> lstSubjects = (List<SubjectEntity>)subject.GetData();

            return View(lstSubjects[0]);
        }

        // POST: Subject/Delete
        public JsonResult Delete(int? IdSubject)
        {
            Subjects subject = new Subjects(IdSubject);
            var ret = subject.Remove();

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
