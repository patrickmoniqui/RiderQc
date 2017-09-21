using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IRideRepository
    {
        bool Create(RideViewModel rideViewModel);
        bool Delete(int rideId);
        Ride Get(int rideId);
        List<Ride> GetAllRides();
    }
}
