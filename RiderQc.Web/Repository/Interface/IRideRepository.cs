using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IRideRepository
    {
        bool Create(RideCreateViewModel rideViewModel);
        bool Update(RideCreateViewModel rideCreateView);
        bool CreateWithTrajet(RideCreateWithTrajetViewModel rideViewModel);
        bool Delete(int rideId);
        bool Exist(int rideId);
        bool UserIsCreator(int rideId, string username);
        RideViewModel Get(int rideId);
        List<RideViewModel> GetAllRides();
        List<RideViewModel> MyRidesForUser(string username);
        bool AddUserToParticipants(int rideId, string username);
        bool RemoveUserToParticipants(int rideId, string username);
        List<string> GetPartipants(int rideId);
    }
}
