using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    public class departmentClass
    {
        SJGHLINQDataContext objLinq = new SJGHLINQDataContext();

        public List<string> getDepartmentList()
        {
            List<string> deptList = new List<string>();
            foreach (var dept in objLinq.Departments.Select(x => x.name))
            {
                deptList.Add(dept);
            }
            return deptList;
        }
    }
}