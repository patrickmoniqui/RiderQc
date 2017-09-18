using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.DAL
{
    public class RideDao : IRideDao
    {
        public List<Ride> GetAllRides()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Rides.ToList();
            }
        }
    }
}