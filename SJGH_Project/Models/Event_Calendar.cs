using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    [MetadataType(typeof(Event_Calendar_Validation))]
    public partial class Event_Calendar
    {
        SJGHLINQDataContext db = new SJGHLINQDataContext();

        public IQueryable<Event_Calendar> GetAll()
        {
            return db.Event_Calendars.OrderByDescending(x => x.start_date);
        }

        public Event_Calendar GetByID(int id)
        {
            return db.Event_Calendars.SingleOrDefault(x => x.event_id == id);
        }

        public bool Delete(int id)
        {
            try
            {
                Event_Calendar obj = GetByID(id);
                if (obj != null)
                {
                    db.Event_Calendars.DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Edit(int id, Event_Calendar obj)
        {
            Event_Calendar original = db.Event_Calendars.SingleOrDefault(x => x.event_id == id);

            if (original != null)
            {
                original.location_id = obj.location_id;
                original.name = obj.name;
                original.start_date = obj.start_date;
                original.end_date = obj.end_date;
                original.start_time = obj.start_time;
                original.end_time = obj.end_time;
                original.description = obj.description;
                original.image_url = obj.image_url;

                db.SubmitChanges();
            }
        }

        public void Insert(Event_Calendar obj)
        {
            db.Event_Calendars.InsertOnSubmit(obj);
            db.SubmitChanges();
        }
    }

    public class Event_Calendar_Validation
    {
        [DisplayName("Location")]
        [Required(ErrorMessage = "Required Field!")]
        public int location_id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Required Field!")]
        public string name { get; set; }

        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime start_date { get; set; }

        [DisplayName("End Date")]
        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime end_date { get; set; }

        [DisplayName("Start Time")]
        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan start_time { get; set; }

        [DisplayName("End Time")]
        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan end_time { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Required Field!")]
        public string description { get; set; }

        [DisplayName("Image")]
        public string image_url { get; set; }
    }
}