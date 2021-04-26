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
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult Index()
        {
            return View();
        }

        // GET: Registration/GetRegistration
        [HttpGet]
        public JsonResult GetRegistration(int? IdRegistration)
        {
            Registration regis = new Registration(IdRegistration);
            List<RegistrationEntity> lstRegistration = (List<RegistrationEntity>)regis.GetData();

            return new JsonResult { Data = lstRegistration, JsonRequestBehavior = JsonRequestBehavior.AllowGet };            
        }

        // GET: Registration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Registration/Save
        [HttpPost]
        public JsonResult Save(Registration Registration)
        {
            try
            {
                Registration regis = new Registration(Registration);
                var ret = regis.Save();

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
        public ActionResult Edit(int? IdRegistration)
        {
            Registration regis = new Registration(IdRegistration);
            List<RegistrationEntity> lstRegistration = (List<RegistrationEntity>)regis.GetData();
            return View(lstRegistration[0]);
        }

        // POST: Registration/Delete
        public JsonResult Delete(int? IdRegistration)
        {
            Registration regis = new Registration(IdRegistration);
            var ret = regis.Remove();

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
