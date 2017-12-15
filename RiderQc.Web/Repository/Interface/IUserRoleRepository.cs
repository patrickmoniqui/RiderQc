using RiderQc.Web.Entities;

namespace RiderQc.Web.Repository.Interface
{
    public interface IUserRoleRepository
    {
        bool CreateUserRole(int userId, int roleId);
        UserRole GetUserRole(int userId);
    }
}
