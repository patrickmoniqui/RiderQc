using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IUserRepository
    {
        bool RegisterUser(UserRegisterViewModel user);
        bool DeleteUser(string username);
        bool CheckUserExistence(string username);
        User GetUserById(int userId);
        List<User> GetAllUsers();
        List<Trajet> GetAllTrajets();
        List<Ride> GetAllRides();
    }
}
