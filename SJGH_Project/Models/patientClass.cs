using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    public class patientClass
    {
        SJGHLINQDataContext objLinq = new SJGHLINQDataContext();

        public IQueryable<Patient> getAllPatients()
        {
            var allPatients = objLinq.Patients.Select(x => x);
            return allPatients;
        }

        public bool commitInsert(Patient pat)
        {
            using (objLinq)
            {
                objLinq.Patients.InsertOnSubmit(pat);
                objLinq.SubmitChanges();
                return true;
            }
        }

        public Patient getPatientByUserName(string _username)
        {
            var allPatients = objLinq.Patients.SingleOrDefault(x => x.username == _username);
            return allPatients;
        }

        public Patient getPatientByID(int _id)
        {
            var allPatients = objLinq.Patients.SingleOrDefault(x => x.patient_id == _id);
            return allPatients;
        }
    }
}