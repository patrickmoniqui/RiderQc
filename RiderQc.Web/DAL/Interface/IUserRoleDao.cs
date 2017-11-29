using RiderQc.Web.Entities;

namespace RiderQc.Web.DAL.Interface
{
    public interface IUserRoleDao
    {
        UserRole GetUserRole(int userId);
        bool CreateUserRole(int userId, int roleId);
    }
}
