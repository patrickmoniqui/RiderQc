using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace RiderQc.Web.DAL
{
    public class RideDao : IRideDao
    {
        public List<Ride> GetAllRides()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Rides
                    .Include(x => x.User)
                    .Include(x => x.Level)
                    .Include(x => x.Trajet)
                    .ToList();
            }
        }
    }
}