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
    public class DoctorAccountController : Controller
    {
        doctorClass objDoc = new doctorClass();
        departmentClass objDept = new departmentClass();
        
        //
        // GET: /DoctorAccount/
        [Authorize(Roles="doctor")]
        public ActionResult Index()
        {
            string username = User.Identity.Name.ToString();
            return View(objDoc.getDoctorByUserName(username));
        }

        //
        // GET: /DoctorAccount/
        [Authorize(Roles = "doctor")]
        public ActionResult DoctorProfile()
        {
            string username = User.Identity.Name.ToString();
            return View(objDoc.getDoctorByUserName(username));
        }

        //
        // GET: /DoctorAccount/UpdateDoctor
        [Authorize(Roles = "doctor")]
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
        // POST: /DoctorAccount/UpdateDoctor
        [HttpPost]
        [Authorize(Roles = "doctor")]
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

                    return RedirectToAction("DoctorProfile");
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
