using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IUserDao
    {
        bool RegisterUser(User user);
        bool DeleteUser(string username);
        bool CheckUserExistence(string username);
        bool CredentialsAreValid(string username, string password);
        User GetByUsername(string username);
        User GetUserById(int user_id);
        List<User> GetAllUsers();
        List<Trajet> GetAllTrajets();
        List<Ride> GetAllRides();
        AuthentificationTokenViewModel GenerateTokenForUser(string username);
        AuthentificationTokenViewModel GetLastValidTokenByUsername(string username);
        User GetUserByTokenIsLastTokenIsValid(string token);
    }
}
