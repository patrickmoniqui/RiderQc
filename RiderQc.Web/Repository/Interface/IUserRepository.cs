using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.Trajet;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IUserRepository
    {
        bool RegisterUser(UserRegisterViewModel user);
        bool DeleteUser(string username);
        bool CheckUserExistence(string username);
        UserViewModel GetUserById(int userId);
        List<UserViewModel> GetAllUsers();
        List<TrajetViewModel> GetAllTrajets();
        List<RideViewModel> GetAllRides();
    }
}
