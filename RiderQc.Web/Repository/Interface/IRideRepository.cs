using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IRideRepository
    {
        bool Create(RideViewModel rideViewModel);
        bool Delete(int rideId);
        bool UserIsCreator(int rideId, string username);
        RideViewModel Get(int rideId);
        List<RideViewModel> GetAllRides();
    }
}
