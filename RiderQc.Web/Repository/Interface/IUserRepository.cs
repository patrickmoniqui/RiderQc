using RiderQc.Web.ViewModels.Admin;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IUserRepository
    {
        bool RegisterUser(UserRegisterViewModel user);
        bool DeleteUser(string username);
        bool CheckUserExistence(string username);
        bool CredentialsAreValid(string username, string password);
        UserViewModel GetUserById(int userId);
        UserViewModel GetUserByName(string username);
        List<UserViewModel> GetAllUsers();
        AuthentificationTokenViewModel GenerateTokenForUser(string username);
        AuthentificationTokenViewModel GetLastValidTokenByUsername(string username);
        UserViewModel GetUserByTokenIfLastTokenIsValid(string token);
        UserAdminViewModel GetUserAdminById(int userId);
    }
}
