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
    public class AdminUserController : Controller
    {
        doctorClass objDoc = new doctorClass();
        patientClass objPat = new patientClass();
        departmentClass objDept = new departmentClass();
        
        //
        // GET: /AdminUser/
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            return View();
        }


        //
        // GET: /Admin/ListDoctor
        [Authorize(Roles = "admin")]
        public ActionResult ListDoctor()
        {
            return View(objDoc.getAllDoctors());
        }

        //
        // GET: /Admin/ListPatient
        [Authorize(Roles = "admin")]
        public ActionResult ListPatient()
        {
            return View(objPat.getAllPatients());
        }

        //
        // GET: /Admin/DoctorDetail
        [Authorize(Roles = "admin")]
        public ActionResult DoctorDetail(int id)
        {
            return View(objDoc.getDoctorByID(id));
        }

        //
        // GET: /Admin/PatientDetail
        [Authorize(Roles = "admin")]
        public ActionResult PatientDetail(int id)
        {
            return View(objPat.getPatientByID(id));
        }


        //
        // GET: /Admin/UpdateDoctor
        [Authorize(Roles = "admin")]
        public ActionResult UpdateDoctor(int id)
        {
            ViewBag.doctorId = id;
            //Get the department list from dapartment table and pass it to view using viewbag
            ViewBag.DepartmentList = objDept.getDepartmentList();

            //Get the doctor from database based on the selected doctor id
            var objDoctor = objDoc.getDoctorByID(id);
            if (objDoctor == null)
            {
                return View("NotFound");
            }
            else
            {
                UpdateDoctorModel objUpDoc = new UpdateDoctorModel();
                objUpDoc.Department = objDoctor.department_name;
                objUpDoc.FirstName = objDoctor.firstname;
                objUpDoc.LastName = objDoctor.lastname;
                objUpDoc.Email = objDoctor.email;
                objUpDoc.Phone = objDoctor.phone;
                objUpDoc.Specialty = objDoctor.specialty;
                objUpDoc.Bio = objDoctor.bio;
                objUpDoc.Photo_url = objDoctor.photo_url;
                objUpDoc.Status = Convert.ToInt32(objDoctor.status);
                return View(objUpDoc);
            }
        }


        //
        // POST: /Admin/UpdateDoctor
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateDoctor(int id, UpdateDoctorModel model)
        {
            ViewBag.doctorId = id;
            if (ModelState.IsValid)
            {
                try
                {
                    SJGHLINQDataContext objLinq = new SJGHLINQDataContext();
                    Doctor objDoctor = objLinq.Doctors.Single(x => x.doctor_id == id);
                    objDoctor.department_name = model.Department;
                    objDoctor.firstname = model.FirstName;
                    objDoctor.lastname = model.LastName;
                    objDoctor.email = model.Email;
                    objDoctor.phone = model.Phone;
                    objDoctor.specialty = model.Specialty;
                    objDoctor.bio = model.Bio;
                    objDoctor.photo_url = model.Photo_url;
                    objDoctor.status = model.Status;

                    objLinq.SubmitChanges();

                    return RedirectToAction("DoctorDetail/" + id);
                }
                catch
                {
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Admin/UpdatePatient
        [Authorize(Roles = "admin")]
        public ActionResult UpdatePatient(int id)
        {
            ViewBag.patientId = id;

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
        // POST: /Admin/UpdatePatient
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePatient(int id, UpdatePatientModel model)
        {
            ViewBag.patientId = id;
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

                    return RedirectToAction("PatientDetail/" + id);
                }
                catch
                {
                    return View(model);
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        //
        // GET: /Admin/CreateDoctor
        [Authorize(Roles = "admin")]
        public ActionResult CreateDoctor()
        {
            //Get the department list from dapartment table and pass it to view using viewbag
            ViewBag.DepartmentList = objDept.getDepartmentList();
            return View();
        }


        //
        // POST: /Admin/CreateDoctor
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDoctor(CreateDoctorModel model)
        {
            if (ModelState.IsValid)
            {
                // Attempt to register the user
                try
                {
                    WebSecurity.CreateUserAndAccount(model.UserName, model.Password);

                    //Add a role to the new user
                    Roles.AddUserToRole(model.UserName, "Doctor");

                    //Create doctor object based on user input
                    Doctor objDoctor = new Doctor();
                    objDoctor.username = model.UserName;
                    objDoctor.department_name = model.Department;
                    objDoctor.firstname = model.FirstName;
                    objDoctor.lastname = model.LastName;
                    objDoctor.email = model.Email;
                    objDoctor.phone = model.Phone;
                    objDoctor.specialty = model.Specialty;
                    objDoctor.bio = model.Bio;
                    objDoctor.photo_url = model.Photo_url;
                    objDoctor.status = 1;

                    //Insert doctor object to database doctor table

                    objDoc.commitInsert(objDoctor);

                    return RedirectToAction("ListDoctor", "AdminUser");
                }
                catch (MembershipCreateUserException e)
                {
                    ModelState.AddModelError("", ErrorCodeToString(e.StatusCode));
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

    }
}
