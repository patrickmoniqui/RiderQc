using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System;

namespace RiderQc.Web.DAL
{
    public class RideDao : IRideDao
    {
        public bool Create(Ride ride)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Rides.Add(ride);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public bool Delete(int rideId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Ride ride = ctx.Rides.Find(rideId);

                if(ride == null)
                {
                    return false;
                }

                ctx.Rides.Remove(ride);
                int result = ctx.SaveChanges();

                return result == 1 ? true : false;
            }
        }

        public Ride Get(int rideId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                return ctx.Rides.Find(rideId);
            }
        }

        public List<Ride> GetAllRides()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var rides = ctx.Rides
                    .Include(x => x.User)
                    .Include(x => x.Level)
                    .Include(x => x.Trajet)
                    .Include(x => x.Comments);

                if(rides != null)
                {
                    return rides.ToList();
                }
                else
                {
                    return new List<Ride>();
                }
            }
        }
    }
}