using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJGH_Project.Models
{
    public class locationClass
    {
        SJGHLINQDataContext objLinq = new SJGHLINQDataContext();

        public IQueryable<Location> getAllLocation()
        {
            var allLocations = objLinq.Locations.Select(x => x);
            return allLocations;
        }
    }
}