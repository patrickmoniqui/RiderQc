using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;

namespace RiderQc.Web.DAL
{
    public class UserRoleDao : IUserRoleDao
    {
        public bool CreateUserRole(int userId, int roleId)
        {
            UserRole userRole = new UserRole();
            userRole.UserId = userId;
            userRole.RoleId = roleId;

            int result = -1;

            using (RiderQcContext ctx = new RiderQcContext())
            {
                ctx.UserRoles.Add(userRole);
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

        //Gets a User's role, can be used to validate if Admin or Regular user.
        public UserRole GetUserRole(int userId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
                return ctx.UserRoles.FirstOrDefault(e => e.UserId == userId);
        }
    }
}