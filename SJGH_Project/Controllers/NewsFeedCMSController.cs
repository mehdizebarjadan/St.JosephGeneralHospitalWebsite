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

// import models
using SJGH_Project.Models;

namespace SJGH_Project.Controllers
{
    public class NewsFeedCMSController : Controller
    {
        // new instance of our news class
        newsClass objNewsCMS = new newsClass();

        //get All News
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            var news = objNewsCMS.getAllNews();
            return View(news);
        }
        
        //Get Individual News
        [Authorize(Roles = "Admin")]
        public ActionResult newsDetails(int id)
        {
            var news = objNewsCMS.getNewsById(id);
            if (news == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(news);
            }
        } // end news details

        // news insert page
        [Authorize(Roles = "Admin")]
        public ActionResult newsInsert()
        {
            return View();

        }// end newsInsert

        //news insert page after form submission
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult newsInsert(NeewFeed newsItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {   
                    

                    // IMAGES
                    if (file != null)
                    {
                        // get the file name
                        string photo = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(
                                   Server.MapPath("~/Images/NewsFeed"), photo);

                        // file is uploaded
                        file.SaveAs(path);

                        // SAve Path in DB
                        newsItem.photo_url = "~/Images/NewsFeed/" + file.FileName;

                    }
                    else
                    {   
                        // if there is no file, default string kicks in
                        newsItem.photo_url = "~/Images/NewsFeed/news_default.jpg";
                    }

                    //AUTHOR
                    if (newsItem.author == null)
                    {
                        newsItem.author = "SJGH";
                    }


                    objNewsCMS.commitNewsInsert(newsItem);
                    return RedirectToAction("Index");
                }

                catch
                {
                    return View();
                }

            }// end if statement

            return View();

        }// end ActionResult news Insert

        // controller for Update
        [Authorize(Roles = "Admin")]
        public ActionResult newsUpdate(int id)
        {
            var news = objNewsCMS.getNewsById(id);
            if (news == null)
            {
                return View("Not Found");
            }
            else
            {
                return View(news);
            }
        
        }// end news update pre submit

        // Update post submit. Passind id arg, table obj and image posting
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult newsUpdate(int id, NeewFeed newsItem, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    // IMAGES
                    if (file != null)
                    {
                        // get the file name
                        string photo = System.IO.Path.GetFileName(file.FileName);
                        string path = System.IO.Path.Combine(
                                   Server.MapPath("~/Images/NewsFeed"), photo);

                        // file is uploaded
                        file.SaveAs(path);

                        // SAve Path in DB
                        newsItem.photo_url = "~/Images/NewsFeed/" + file.FileName;

                    }


                    // ============= EXECUTE UPDATE QUERY
                    //objNewsCMS.commitNewsUpdate(id, newsItem.headline, newsItem.body, newsItem.date, newsItem.author, newsItem.published, newsItem.photo_url, newsItem.category);

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            
            }// end if statement

            return View();
        
        }// end newsUpdate HTTPOST
        



        // COntroller for delete
        [Authorize(Roles = "Admin")]
        public ActionResult newsDelete(int id)
        {
            var news = objNewsCMS.getNewsById(id);
            if (news == null)
            {
                return View("NotFound");
            }
            else
            {
                return View(news);
            }
        }// end delete pre submit

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult newsDelete(int id, NeewFeed newsItem)
        {
            try
            {
                // commit and redirect if success
                objNewsCMS.commitNewsDelete(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        
        }// end delete after post

        public ActionResult NotFound()
        {
            return View();
        }// end not found


    } // end newsclass
} // end namespace
