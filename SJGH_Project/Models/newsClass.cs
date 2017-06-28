using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    public class newsClass
    {
        // Database Connection instance of the linq Object
        SJGHLINQDataContext objNews = new SJGHLINQDataContext();

        // calling the table from database
        public IQueryable<NeewFeed>getNews()
        { 
            // creating anonymous variables order by Id Descending for published bews
            var allNews = objNews.NeewFeeds.OrderByDescending(x => x.date).Select(x => x).Where(x => x.published == "yes");

            // return information from query
            return allNews;

        } // end of IQueryable

        // get Details by ID
        public NeewFeed getNewsById(int _id)
        {
            var allNews = objNews.NeewFeeds.SingleOrDefault(x => x.news_id == _id);
            return allNews;

        }// end News Feed Get by Id


        // fetching featured news for rotating news feed in home page
        // only published and featured news

        public IQueryable<NeewFeed>getFeaturedNews()
        {
            // creating anonymous variables order by Id Descending for published bews
            var allNews = objNews.NeewFeeds.OrderByDescending(x => x.date).Select(x => x).Where(x => x.published == "yes").Where(x => x.category == "featured");

            // return information from query
            return allNews;
        
        } // end IQuerable

        // == CMS ==//
         // get All news in database for Index
        public IQueryable<NeewFeed> getAllNews()
        {
            // creating anonymous variables order by date Descending for published bews
            var allNews = objNews.NeewFeeds.Select(x => x).OrderByDescending(x => x.date);

            // return information from query
            return allNews;

        } // end of IQueryable
         

        // == DO INSERT 

        public bool commitNewsInsert(NeewFeed news)
            //creating instance of news table
        {
            using (objNews)
            {   
                //Using Model to set columns
                objNews.NeewFeeds.InsertOnSubmit(news);

                //Commit Changes into table
                objNews.SubmitChanges();
                return true;

            }// end using
        }// end commitNewsInsert

        // DO UPDATE

        public bool commitNewsUpdate(int _id, string _headline, string _body, DateTime _date, string _author, string _published, string _photo_url, string _category)
        {
            using (objNews)
            {   
                // variable that allows single out news
                var ObjNewsUp = objNews.NeewFeeds.Single(x => x.news_id == _id);
                
                // set tables columns to new values
                ObjNewsUp.headline = _headline;
                ObjNewsUp.body = _body;
                ObjNewsUp.date = _date;
                ObjNewsUp.author = _author;
                ObjNewsUp.published = _published;
                ObjNewsUp.photo_url = _photo_url;
                ObjNewsUp.category = _category;

                // commit update agains database
                objNews.SubmitChanges();
                return true;

            }// end using
        
        }// end Updte

        // DO DELETE

        public bool commitNewsDelete(int _id)
        {
            using (objNews)
            {
                var ObjNewsDel = objNews.NeewFeeds.Single(x => x.news_id == _id);
                // delete command
                objNews.NeewFeeds.DeleteOnSubmit(ObjNewsDel);
                 // commit changes against the database
                objNews.SubmitChanges();
                return true;

            }// end using
        }

    } // end newsClass
} // namespace