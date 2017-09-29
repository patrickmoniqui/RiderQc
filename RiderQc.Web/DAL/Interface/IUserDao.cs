using RiderQc.Web.Entities;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IUserDao
    {
        bool RegisterUser(User user);
        bool DeleteUser(string username);
        bool CheckUserExistence(string username);
        List<User> GetAllUsers();
        List<Trajet> GetAllTrajets();
        List<Ride> GetAllRides();

    }
}
