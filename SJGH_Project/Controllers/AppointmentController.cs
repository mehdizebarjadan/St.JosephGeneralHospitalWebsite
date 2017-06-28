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

namespace SJGH_Project.Controllers
{
    public class AppointmentController : Controller
    {
        appointmentClass objApp = new appointmentClass();
        patientClass objPat = new patientClass();
        doctorClass objDoc = new doctorClass();
        locationClass objLoc = new locationClass();
        
        //
        // GET: /Appointment/ShowPatientAppointment
        [Authorize(Roles="patient")]
        public ActionResult ShowPatientAppointment()
        {
            int patientId = objPat.getPatientByUserName(User.Identity.Name).patient_id;
            return View(objApp.getAppointmentsByPatientID(patientId));
        }

        //
        // GET: /Appointment/PatientCreateAppointment
        [Authorize(Roles="patient")]
        public ActionResult PatientCreateAppointment()
        {
            ViewBag.available = true;
            ViewBag.doctorList = objDoc.getAllDoctors();
            ViewBag.locationList = objLoc.getAllLocation();
            return View();
        }

        //
        // POST: /Appointment/PatientCreateAppointment
        [HttpPost]
        [Authorize(Roles = "patient")]
        public ActionResult PatientCreateAppointment(CreateAppointmentModel app)
        {
            ViewBag.doctorList = objDoc.getAllDoctors();
            ViewBag.locationList = objLoc.getAllLocation();
            if (objApp.checkDoctorAvailability(app.doctorId, app.date, app.time))
            {
                Appointment appCreate = new Appointment();
                appCreate.patient_id = objPat.getPatientByUserName(User.Identity.Name).patient_id;
                appCreate.doctor_id = app.doctorId;
                appCreate.location_id = app.locatioId;
                appCreate.date = app.date;
                appCreate.time = app.time;
                appCreate.status = "Upcoming";
                app.additoinInfo = app.additoinInfo;

                objApp.commitInsert(appCreate);
                return RedirectToAction("ShowPatientAppointment");
            }
            else
            {
                ViewBag.available = false;
                return View(app);
            }
            
        }

        //
        // GET: /Appointment/PatientAppointmentDetail
        [Authorize(Roles = "patient")]
        public ActionResult PatientAppointmentDetail(int _id)
        {
            ShowPatientAppointmentModel appDetail = new ShowPatientAppointmentModel();
            var userRole = Roles.GetRolesForUser(User.Identity.Name)[0];IEnumerable<ShowPatientAppointmentModel> showAppList = objApp.getAppointmentsByPatientID(objPat.getPatientByUserName(User.Identity.Name).patient_id);
            
            foreach (var app in showAppList)
            {
                if (app.appointmentId == _id)
                {
                    appDetail = app;
                }
            }
            return View(appDetail);
        }


        //
        // GET: /Appointment/PatientAppointmentDetail
        [Authorize(Roles = "patient")]
        public ActionResult CancelPatientAppointment(int _id)
        {
            ViewBag.appointmentId = _id;
            return View(objApp.getAppointmentByID(_id));
        }


        //
        // POST: /Appointment/CancelPatientAppointment
        [HttpPost]
        [Authorize(Roles = "patient")]
        public ActionResult CancelPatientAppointment(int _id, Appointment model)
        {
            ViewBag.appointmentId = _id;
            objApp.deleteAppointment(_id);
            return RedirectToAction("ShowPatientAppointment");
        }
    }
}
