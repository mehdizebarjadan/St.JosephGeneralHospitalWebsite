using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    public class doctorClass
    {
        SJGHLINQDataContext objLinq = new SJGHLINQDataContext();

        public IQueryable<Doctor> getAllDoctors()
        {
            var allDoctors = objLinq.Doctors.Select(x => x);
            return allDoctors;
        }

        public Doctor getDoctorByID(int _id)
        {
            var allDoctors = objLinq.Doctors.SingleOrDefault(x => x.doctor_id == _id);
            return allDoctors;
        }

        public Doctor getDoctorByUserName(string _username)
        {
            var allDoctors = objLinq.Doctors.SingleOrDefault(x => x.username == _username);
            return allDoctors;
        }

        public bool commitInsert(Doctor doc)
        {
            using (objLinq)
            {
                objLinq.Doctors.InsertOnSubmit(doc);
                objLinq.SubmitChanges();
                return true;
            }
        }
    }
}