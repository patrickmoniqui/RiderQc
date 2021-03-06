﻿using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;
using System.Collections.Generic;
using System;
using RiderQc.Web.Helpers;
using RiderQc.Web.ViewModels.User;
using System.Data.Entity;

namespace RiderQc.Web.DAL
{
    public class UserDao : IUserDao
    {
        public bool Delete(string username)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                if(user != null)
                {
                    user.IsActive = false;
                    ctx.Entry(user).State = EntityState.Modified;
                    result = ctx.SaveChanges();
                }
            }

            if (result >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RegisterUser(User user)
        {
            int result = -1;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                user.Password = EncryptionHelper.HashToSHA256(user.Password);
                user.IsActive = true;
                ctx.Users.Add(user);
                result = ctx.SaveChanges();
            }

            if (result <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool EditUser(User user)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User _user = ctx.Users.FirstOrDefault(x => x.UserID == user.UserID);

                if(_user != null)
                {
                    _user.DateOfBirth = user.DateOfBirth;
                    _user.Description = user.Description;
                    _user.DpUrl = user.DpUrl;
                    _user.Region = user.Region;
                    _user.Ville = user.Ville;

                    ctx.Entry(_user).State = EntityState.Modified;
                    result = ctx.SaveChanges();
                }
                
            }

            if (result <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool CheckUserExistence(string username)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                foreach (User u in ctx.Users)
                {
                    if (u.Username == username)
                        return true;
                }

            }
            return false;
        }

        public User GetUserById(int userId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User user = ctx.Users
                     .Include(x => x.Motoes)
                     .FirstOrDefault(x => x.UserID == userId);

                if(user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var users = ctx.Users
                    .Include(u => u.Motoes)
                    .Where(x => x.IsActive);

                if (users != null)
                {
                    return users.ToList();
                }
                else
                {
                    return new List<User>();
                }
            }
        }
        
        public bool CredentialsAreValid(string username, string password)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                bool result = false;

                string hashedPwd = EncryptionHelper.HashToSHA256(password);

                result = ctx.Users.Any(x => x.Username == username && x.Password == hashedPwd);

                return result;
            }
        }

        public User GetByUsername(string username)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User user = null;

                user = ctx.Users
                    .Include(x => x.Motoes)
                    .FirstOrDefault(x => x.Username == username);

                if(user != null)
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }

        public AuthentificationTokenViewModel GenerateTokenForUser(string username, int expiresAfterNbDays = 30)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                // If user not existing
                if (user == null)
                {
                    return null;
                }

                Authentification authToken = new Authentification();
                authToken.UserId = user.UserID;
                authToken.IssueDate = DateTime.Now;
                authToken.ExpirationDate = DateTime.Now.AddDays(expiresAfterNbDays);
                authToken.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                
                user.Authentifications.Add(authToken);
                ctx.SaveChanges();

                AuthentificationTokenViewModel tokenViewModel = new AuthentificationTokenViewModel();
                tokenViewModel.Token = authToken.Token;
                tokenViewModel.IssueDate = authToken.IssueDate;
                tokenViewModel.ExpirationDate = authToken.ExpirationDate;

                return tokenViewModel;
            }
        }

        public AuthentificationTokenViewModel GetLastValidTokenByUsername(string username)
        {
            AuthentificationTokenViewModel token = null;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                User user = ctx.Users
                    .Include("Authentifications")
                    .FirstOrDefault(x => x.Username == username);

                if (user != null)
                {
                    var listToken = user.Authentifications.OrderByDescending(x => x.IssueDate);

                    if (listToken?.Count() >= 1)
                    {
                        Authentification lastToken = listToken.FirstOrDefault();

                        //Token still good
                        if (lastToken.ExpirationDate > DateTime.Now)
                        {
                            token = new AuthentificationTokenViewModel();
                            token.Token = lastToken.Token;
                            token.IssueDate = lastToken.IssueDate;
                            token.ExpirationDate = lastToken.ExpirationDate;
                        }
                    }
                }
            }

            return token;
        }

        public User GetUserByTokenIsLastTokenIsValid(string token)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                Authentification auth = ctx.Authentifications
                    .Include("User")
                    .FirstOrDefault(x => x.Token == token && x.ExpirationDate > DateTime.Now);

                if (auth != null && auth.User != null)
                {
                    return auth.User;
                }
                else
                {
                    return null;
                }
            }
        }

        public List<Ride> GetMyRides(string username)
        {
            List<Ride> rides = null;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                if(!CheckUserExistence(username))
                {
                    return null;
                }

                User user = ctx.Users
                    .Include(x => x.MyRides)
                    .FirstOrDefault(x => x.Username == username);

                rides = user.MyRides.ToList();
            }

            return rides;
        }

        public bool EditUserPwd(string username, string pwd)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User _user = ctx.Users.FirstOrDefault(x => x.Username == username);

                if (_user != null)
                {
                    string hashedPwd = EncryptionHelper.HashToSHA256(pwd);
                    _user.Password = hashedPwd;

                    ctx.Entry(_user).State = EntityState.Modified;
                    result = ctx.SaveChanges();
                }
            }

            if (result <= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}