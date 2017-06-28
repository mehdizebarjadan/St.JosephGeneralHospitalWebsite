using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    [MetadataType(typeof(Seasonal_Dessese_Validation))]
    public partial class Seasonal_Dessese
    {
        SJGHLINQDataContext db = new SJGHLINQDataContext();

        public IQueryable<Seasonal_Dessese> GetAll()
        {
            return db.Seasonal_Desseses.OrderByDescending(x => x.start_date);
        }

        public Seasonal_Dessese GetByID(int id)
        {
            return db.Seasonal_Desseses.SingleOrDefault(x => x.id == id);
        }

        public bool Delete(int id)
        {
            try
            {
                Seasonal_Dessese obj = GetByID(id);
                if (obj != null)
                {
                    db.Seasonal_Desseses.DeleteOnSubmit(obj);
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

        public void Edit(int id, Seasonal_Dessese obj)
        {
            Seasonal_Dessese original = db.Seasonal_Desseses.SingleOrDefault(x => x.id == id);

            if (original != null)
            {
                original.start_date = obj.start_date;
                original.start_time = obj.start_time;
                original.topic = obj.topic;
                original.description = obj.description;
                original.image_url = obj.image_url;

                db.SubmitChanges();
            }
        }

        public void Insert(Seasonal_Dessese obj)
        {
            db.Seasonal_Desseses.InsertOnSubmit(obj);
            db.SubmitChanges();
        }
    }

    public class Seasonal_Dessese_Validation
    {
        [DisplayName("Start Date")]
        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime start_date { get; set; }

        [DisplayName("Start Time")]
        [Required(ErrorMessage = "Required Field!")]
        [DataType(DataType.Time)]
        //[RegularExpression(@"^(0[1-9]|1[0-2]):[0-5][0-9] (am|pm|AM|PM)$", ErrorMessage = "Invalid Time.")]
        //[DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan start_time { get; set; }

        [DisplayName("Topic")]
        [Required(ErrorMessage = "Required Field!")]
        public string topic { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Required Field!")]
        public string description { get; set; }

        [DisplayName("Image")]
        public string image_url { get; set; }
    }
}