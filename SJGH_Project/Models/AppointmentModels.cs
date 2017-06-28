using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SJGH_Project.Models
{
    public class ShowPatientAppointmentModel
    {
        public int appointmentId { get; set; }
        public string doctorFirstName { get; set; }
        public string doctorLastName { get; set; }
        public string locationName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string province { get; set; }
        public string postal { get; set; }
        public DateTime date { get; set; }
        public string time { get; set; }
        public string additionalInfo { get; set; }
        public string status { get; set; }

    }

    public class CreateAppointmentModel
    {
        [Display(Name = "Choose a doctor")]
        [Required(ErrorMessage = "Please choose a doctor")]
        public int doctorId { get; set; }

        [Display(Name = "Choose a location")]
        [Required(ErrorMessage = "Please choose a location")]
        public int locatioId { get; set; }

        [Display(Name = "Choose a date")]
        [Required(ErrorMessage = "Please choose a date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime date { get; set; }

        [Display(Name = "Choose a time")]
        [Required(ErrorMessage = "Please choose a time")]
        public string time { get; set; }

        [Display(Name = "Additional information")]
        public string additoinInfo { get; set; }

    }

}