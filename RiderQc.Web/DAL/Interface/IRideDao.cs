using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IRideDao
    {
        bool Create(Ride ride);
        bool Update(Ride ride);
        bool Delete(int rideId);
        bool Exist(int rideId);
        RideViewModel Get(int rideId);
        List<RideViewModel> GetAllRides();
        bool UserIsCreator(int rideId, string username);
        bool AddUserToParticipants(int rideId, string username);
        bool RemoveUserToParticipants(int rideId, string username);
        List<string> GetPartipants(int rideId);
    }
}
