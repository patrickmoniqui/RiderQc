using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IRideDao
    {
        bool Create(Ride ride);
        bool Delete(int rideId);
        Ride Get(int rideId);
        List<Ride> GetAllRides();
    }
}
