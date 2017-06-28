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
    public class PatientAccountController : Controller
    {

        patientClass objPat = new patientClass();

        //
        // GET: /PatientAccount/
        [Authorize(Roles = "Patient")]
        public ActionResult Index()
        {
            string username = User.Identity.Name.ToString();
            return View(objPat.getPatientByUserName(username));
        }

        //
        // GET: /PatientAccount/PatientProfile
        [Authorize(Roles = "Patient")]
        public ActionResult PatientProfile()
        {
            string username = User.Identity.Name.ToString();
            return View(objPat.getPatientByUserName(username));
        }

        //
        // GET: /PatientAccount/UpdatePatient
        [Authorize(Roles = "patient")]
        public ActionResult UpdatePatient(int id)
        {
            
            //Get the patient from database based on the selected doctor id
            var objPatient = objPat.getPatientByID(id);
            if (objPatient == null)
            {
                return View("NotFound");
            }
            else
            {
                UpdatePatientModel objUpPat = new UpdatePatientModel();
                objUpPat.HealthCard = objPatient.health_card;
                objUpPat.FirstName = objPatient.firstname;
                objUpPat.LastName = objPatient.lastname;
                objUpPat.DoB = objPatient.dob;
                objUpPat.Email = objPatient.email;
                objUpPat.Phone = objPatient.phone;
                objUpPat.Address = objPatient.address;
                objUpPat.City = objPatient.city;
                objUpPat.Province = objPatient.province;
                objUpPat.Postal = objPatient.postal;
                objUpPat.Status = Convert.ToInt32(objPatient.status);
                return View(objUpPat);
            }
        }

        //
        // POST: /PatientAccount/UpdatePatient
        [HttpPost]
        [Authorize(Roles = "patient")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePatient(int id, UpdatePatientModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    SJGHLINQDataContext objLinq = new SJGHLINQDataContext();
                    Patient objPatient = objLinq.Patients.Single(x => x.patient_id == id);
                    objPatient.health_card = model.HealthCard;
                    objPatient.firstname = model.FirstName;
                    objPatient.lastname = model.LastName;
                    objPatient.dob = model.DoB;
                    objPatient.email = model.Email;
                    objPatient.phone = model.Phone;
                    objPatient.address = model.Address;
                    objPatient.city = model.City;
                    objPatient.province = model.Province;
                    objPatient.postal = model.Postal;
                    objPatient.status = model.Status;

                    objLinq.SubmitChanges();

                    return RedirectToAction("PatientProfile");
                }
                catch
                {
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }
    }
}
