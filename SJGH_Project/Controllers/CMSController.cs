using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using SJGH_Project.Filters;
using SJGH_Project.Models;
using System.IO;
using PagedList;

namespace SJGH_Project.Controllers
{
    public class CMSController : Controller
    {
        

        //
        // GET: /CMS/
        [Authorize(Roles = "admin, doctor")]
        public ActionResult Index()
        {
            return View();
        }

        #region Medical History
        public ActionResult MedicalHistory(int id, int? page)
        {
            ViewBag.Patient = new patientClass().getPatientByID(id);
            var res = new Medical_History().GetByPatientID(id)
                .OrderBy(x => x.date);
            
            int pageSize = 2;

            int pageNumber; //= (page ?? 1);
            if (page == null)
            {
                pageNumber = (res.Count() / pageSize);
                if ((res.Count() % pageSize) > 0)
                    pageNumber++;
            }
            else
            {
                pageNumber = (int)page;
            }

            return View(res.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult DeleteMedicalHistory(int id)
        {
            Medical_History history = new Medical_History().GetByID(id);
            int patientID = history.patient_id;

            Medical_History obj = new Medical_History();
            obj.Delete(id);

            //ViewBag.Patient = new patientClass().getPatientByID(patientID);
            //List<Medical_History> res = new Medical_History().GetByPatientID(patientID).ToList();

            return RedirectToAction("MedicalHistory", new { id = patientID });
        }

        public ActionResult EditMedicalHistory(int id)
        {
            ViewBag.Doctors = new doctorClass().getAllDoctors();

            Medical_History obj = new Medical_History().GetByID(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditMedicalHistory(int id, Medical_History obj)
        {
            int patientID = obj.patient_id;
            obj.Edit(id, obj);
            return RedirectToAction("MedicalHistory", new { id = patientID });
        }

        public ActionResult InsertMedicalHistory(int id)
        {
            Medical_History obj = new Medical_History();
            obj.patient_id = id;

            ViewBag.Doctors = new doctorClass().getAllDoctors();
            return View(obj);
        }

        [HttpPost]
        public ActionResult InsertMedicalHistory(Medical_History obj)
        {
            int patientID = obj.patient_id;
            obj.Insert(obj);
            return RedirectToAction("MedicalHistory", new { id = patientID });
        }
        #endregion

        #region Event Calendar
        public ActionResult EventCalendar(int? page)
        {
            var res = new Event_Calendar().GetAll()
                .OrderBy(x => x.start_date);

            int pageSize = 2;

            int pageNumber; //= (page ?? 1);
            if (page == null)
            {
                pageNumber = (res.Count() / pageSize);
                if ((res.Count() % pageSize) > 0)
                    pageNumber++;
            }
            else
            {
                pageNumber = (int)page;
            }

            return View(res.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult InsertEventCalendar()
        {
            ViewBag.Locations = new locationClass().getAllLocation();
            return View();
        }

        [HttpPost]
        public ActionResult InsertEventCalendar(Event_Calendar obj, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/EventCalendar"), fileName);
                file.SaveAs(path);

                obj.image_url = fileName;
            }

            obj.Insert(obj);
            return RedirectToAction("EventCalendar");
        }

        public ActionResult DeleteEventCalendar(int id)
        {
            string filename = "";
            Event_Calendar obj = new Event_Calendar().GetByID(id);
            if (obj != null)
            {
                if (obj.image_url != null && obj.image_url.Trim() != "")
                {
                    filename = Path.Combine(Server.MapPath("~/Uploads/EventCalendar"), obj.image_url);
                }
                if (obj.Delete(id))
                {
                    if (filename.Trim() != "" && System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                }
            }
            return RedirectToAction("EventCalendar");
        }

        public ActionResult EditEventCalendar(int id)
        {
            ViewBag.Locations = new locationClass().getAllLocation();
            Event_Calendar obj = new Event_Calendar().GetByID(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditEventCalendar(Event_Calendar obj, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (obj.image_url != null && obj.image_url.Trim() != "")
                {
                    string filename = Path.Combine(Server.MapPath("~/Uploads/EventCalendar"), obj.image_url);
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                }
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/EventCalendar"), fileName);
                file.SaveAs(path);

                obj.image_url = fileName;
            }

            obj.Edit(obj.event_id, obj);
            return RedirectToAction("EventCalendar");

        }
        #endregion

        #region Seasonal Dessese
        public ActionResult SeasonalDessese(int? page)
        {
            var res = new Seasonal_Dessese().GetAll()
                .OrderBy(x => x.start_date);

            int pageSize = 2;

            int pageNumber; //= (page ?? 1);
            if (page == null)
            {
                pageNumber = (res.Count() / pageSize);
                if ((res.Count() % pageSize) > 0)
                    pageNumber++;
            }
            else
            {
                pageNumber = (int)page;
            }

            return View(res.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult InsertSeasonalDessese()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InsertSeasonalDessese(Seasonal_Dessese obj, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/SeasonalDessese"), fileName);
                file.SaveAs(path);

                obj.image_url = fileName;
            }

            obj.Insert(obj);
            return RedirectToAction("SeasonalDessese");
        }

        public ActionResult DeleteSeasonalDessese(int id)
        {
            string filename = "";
            Seasonal_Dessese obj = new Seasonal_Dessese().GetByID(id);
            if (obj != null)
            {
                if (obj.image_url != null && obj.image_url.Trim() != "")
                {
                    filename = Path.Combine(Server.MapPath("~/Uploads/SeasonalDessese"), obj.image_url);
                }
                if (obj.Delete(id))
                {
                    if (filename.Trim() != "" && System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                }
            }
            return RedirectToAction("SeasonalDessese");
        }

        public ActionResult EditSeasonalDessese(int id)
        {
            Seasonal_Dessese obj = new Seasonal_Dessese().GetByID(id);
            return View(obj);
        }

        [HttpPost]
        public ActionResult EditSeasonalDessese(Seasonal_Dessese obj, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                if (obj.image_url != null && obj.image_url.Trim() != "")
                {
                    string filename = Path.Combine(Server.MapPath("~/Uploads/SeasonalDessese"), obj.image_url);
                    if (System.IO.File.Exists(filename))
                    {
                        System.IO.File.Delete(filename);
                    }
                }
                var fileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/Uploads/SeasonalDessese"), fileName);
                file.SaveAs(path);

                obj.image_url = fileName;
            }

            obj.Edit(obj.id, obj);
            return RedirectToAction("SeasonalDessese");

        }
        #endregion
    }
}
