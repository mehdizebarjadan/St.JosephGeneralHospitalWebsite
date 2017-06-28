using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    [MetadataType(typeof(Medical_History_Validation))]
    public partial class Medical_History
    {
        SJGHLINQDataContext db = new SJGHLINQDataContext();

        public IQueryable<Medical_History> GetByPatientID(int patientID)
        {
            return db.Medical_Histories
                .Where(x => x.patient_id == patientID);
        }

        public Medical_History GetByID(int id)
        {
            return db.Medical_Histories
                .SingleOrDefault(x => x.history_id == id);
        }

        public void Delete(int id)
        {
            Medical_History obj = GetByID(id);
            if (obj != null){
                db.Medical_Histories.DeleteOnSubmit(obj);
                db.SubmitChanges();
            }
        }

        public void Edit(int id, Medical_History obj)
        {
            Medical_History original = db.Medical_Histories.SingleOrDefault(x => x.history_id == id);

            if (original != null)
            {
                original.doctor_id = obj.doctor_id;
                original.date = DateTime.Now;
                original.description = obj.description;

                db.SubmitChanges();
            }
        }

        public void Insert(Medical_History obj)
        {
            obj.date = DateTime.Now;
            db.Medical_Histories.InsertOnSubmit(obj);
            db.SubmitChanges();
        }
    }

    public class Medical_History_Validation
    {
        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "Please patient name!")]
        public int patient_id { get; set; }

        [DisplayName("Doctor Name")]
        [Required(ErrorMessage = "Please select doctor!")]
        public int doctor_id { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Please enter description!")]
        public string description { get; set; }
    }
}