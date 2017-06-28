using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    public class appointmentClass
    {
        SJGHLINQDataContext objLinq = new SJGHLINQDataContext();

        // Helper function to check if the doctor is available for the date and time chosen by a patient when booking a new appointment
        public bool checkDoctorAvailability(int doctor_id, DateTime date, string time)
        {
            var appointment = objLinq.Appointments.Where(x => x.doctor_id == doctor_id).Where(x => x.date == date).Where(x => x.time == time).Select(x => x);
            if (!appointment.Any())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool commitInsert(Appointment app)
        {
            using (objLinq)
            {
                objLinq.Appointments.InsertOnSubmit(app);
                objLinq.SubmitChanges();
                return true;
            }
        }

        public IQueryable<Appointment> getAllAppointments()
        {
            var allAppointments = objLinq.Appointments.Select(x => x);
            return allAppointments;
        }

        public Appointment getAppointmentByID(int _id)
        {
            var appointment = objLinq.Appointments.SingleOrDefault(x => x.appointment_id == _id);
            return appointment;
        }

        public bool deleteAppointment(int _id)
        {
            using (objLinq)
            {
                var objDelApp = objLinq.Appointments.Single(x => x.appointment_id == _id);
                objLinq.Appointments.DeleteOnSubmit(objDelApp);
                objLinq.SubmitChanges();
                return true;
            }
        }

        // Join the Appointment table with Doctor table and Location table to retrieve doctor names and location names.
        public IQueryable<ShowPatientAppointmentModel> getAppointmentsByPatientID(int _id)
        {
            var allAppointments = from a in objLinq.Appointments.Where(x => x.patient_id == _id).Where(x => x.status == "Upcoming").Select(x => x)
                                  join d in objLinq.Doctors on a.doctor_id equals d.doctor_id
                                  join l in objLinq.Locations on a.location_id equals l.location_id
                                  orderby a.date descending
                                  select new ShowPatientAppointmentModel()
                                  {
                                      appointmentId = a.appointment_id,
                                      doctorFirstName = d.firstname,
                                      doctorLastName = d.lastname,
                                      locationName = l.name,
                                      address = l.address,
                                      city = l.city,
                                      province = l.province,
                                      postal = l.postal,
                                      date = a.date,
                                      time = a.time,
                                      additionalInfo = a.additional_info,
                                      status = a.status
                                  };

            //var allAppointments = objLinq.Appointments.Where(x => x.patient_id == _id).Select(x => x);
            return allAppointments;
        }

        public IQueryable<Appointment> getAppointmentsByDoctorID(int _id)
        {
            var allAppointments = objLinq.Appointments.Where(x => x.doctor_id == _id).Select(x => x);
            return allAppointments;
        }

    }
}