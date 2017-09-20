using System;
using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;

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
    }
}