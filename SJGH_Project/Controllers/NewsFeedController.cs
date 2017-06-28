using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


// using Models
using SJGH_Project.Models;
namespace SJGH_Project.Controllers
{
    public class NewsFeedController : Controller
    {   
        // instance of the newsClass
        newsClass objNewsFeed = new newsClass();

        // index views for news. Get all data from news table
        public ActionResult Index()
        {
            var news = objNewsFeed.getNews();
            return View(news);
        }// end index

        // get details of news using Id as parameter
        public ActionResult NewsPage(int id)
        {
            var news = objNewsFeed.getNewsById(id);
            if (news == null)
            {   
                // if no result will take them to Not Found Page
                return View("NotFound");
            }
            else
            {   
                // return page view with details of the selected views
                return View(news);
            }
        }

        // rotating News Feed 
        public ActionResult _RotatingNewsFeed()
        {
            return PartialView();
        }

 

    }
}
