using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

// using Models
using SJGH_Project.Models;
namespace SJGH_Project.Controllers
{
    public class HomeController : Controller
    {
         // instance of the newsClass
        newsClass objNewsFeed = new newsClass();


        public ActionResult Index()
        {
            //ViewBag.Message = "This is the viewbag Message";
            var news = objNewsFeed.getFeaturedNews();
            return View(news);
        }



        // We can use this for the About Page
        public ActionResult About()
        {
            //ViewBag.Message = "Your app description page.";

            return View();
        }

        // We can use this for the Contact Page
        public ActionResult Contact()
        {
            //ViewBag.Message = "Your contact page.";

            return View();
        }

        //Patient and Visitors Main Menu Item
        public ActionResult PatientsVisitors()
        {
            return View();
        }

        // Facilities Main Menu Item
        public ActionResult Facilities()
        {
            return View();
        }

        //Opportunities Menu Item
        public ActionResult Opportunities()
        {
            return View();
        }
    }
}
