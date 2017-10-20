using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;
using System.Collections.Generic;
using System;
using RiderQc.Web.Helpers;

namespace RiderQc.Web.DAL
{
    public class UserDao : IUserDao
    {
        public bool DeleteUser(string username)
        {
            int result = -1;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.Users.Remove(ctx.Users.FirstOrDefault(x => x.Username == username));
                result = ctx.SaveChanges();
            }

            if (result == 1)
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
                ctx.Users.Add(user);
                result = ctx.SaveChanges();
            }

            if (result == 1)
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
                return ctx.Users.Find(userId);
            }
        }

        public List<User> GetAllUsers()
        {


            using (RiderQcContext ctx = new RiderQcContext())
            {
                var users = ctx.Users;

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

        public List<Trajet> GetAllTrajets()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var trajets = ctx.Trajets;

                if (trajets != null)
                {
                    return trajets.ToList();
                }
                else
                {
                    return new List<Trajet>();
                }
            }
        }

        public List<Ride> GetAllRides()
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                var rides = ctx.Rides;

                if (rides != null)
                {
                    return rides.ToList();
                }
                else
                {
                    return new List<Ride>();
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

                user = ctx.Users.FirstOrDefault(x => x.Username == username);

                return user;
            }
        }

        public string GenerateTokenForUser(string username)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                User user = ctx.Users.FirstOrDefault(x => x.Username == username);

                // If user not existing
                if (user == null)
                {
                    return "";
                }

                Authentification authToken = new Authentification();
                authToken.UserId = user.UserID;
                authToken.IssueDate = DateTime.Now;
                authToken.ExpirationDate = DateTime.Now.AddDays(30);
                authToken.Token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                
                user.Authentifications.Add(authToken);
                ctx.SaveChanges();

                return authToken.Token;
            }
        }

        public string GetLastValidTokenByUsername(string username)
        {
            string token = "";

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
                            token = lastToken.Token;
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
                    .FirstOrDefault(x => x.Token == token && x.ExpirationDate < DateTime.Now);

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
    }
}