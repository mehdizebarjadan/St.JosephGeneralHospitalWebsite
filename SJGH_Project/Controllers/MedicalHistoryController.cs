using SJGH_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SJGH_Project.Controllers
{
    public class MedicalHistoryController : Controller
    {
        //
        // GET: /MedicalHistory/

        public ActionResult Index(int id, int? page)
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

    }
}
