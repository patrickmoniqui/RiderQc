﻿using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Admin;
using RiderQc.Web.ViewModels.Api.User;
using RiderQc.Web.ViewModels.Ride;
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
        bool EditUser(UserViewModel user);
        bool EditUserPwd(string username, string pwd);
        UserViewModel GetUserById(int userId);
        UserViewModel GetUserByName(string username);
        User GetUserByNameAdmin(string username);
        List<UserViewModel> GetAllUsers();
        List<RideViewModel> GetMyRides(string username);
        AuthentificationTokenViewModel GenerateTokenForUser(string username, int expiresAfterNbDays = 30);
        AuthentificationTokenViewModel GetLastValidTokenByUsername(string username);
        UserViewModel GetUserByTokenIfLastTokenIsValid(string token);
        UserAdminViewModel GetUserAdminById(int userId);
        User GetRawUserById(int userId);
    }
}
