using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;

namespace RiderQc.Web.DAL.Interface
{
    public interface IUserDao
    {
        bool RegisterUser(User user);
        bool Delete(string username);
        bool CheckUserExistence(string username);
        bool CredentialsAreValid(string username, string password);
        List<Ride> GetMyRides(string username);
        User GetByUsername(string username);
        User GetUserById(int user_id);
        List<User> GetAllUsers();
        AuthentificationTokenViewModel GenerateTokenForUser(string username, int expiresAfterNbDays = 30);
        AuthentificationTokenViewModel GetLastValidTokenByUsername(string username);
        User GetUserByTokenIsLastTokenIsValid(string token);
    }
}
