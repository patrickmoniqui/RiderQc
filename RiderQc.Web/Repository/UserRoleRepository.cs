using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;

namespace RiderQc.Web.Repository
{
    public class UserRoleRepository : IUserRoleRepository
    {
        private readonly IUserRoleDao dao;

        public UserRoleRepository(IUserRoleDao _dao)
        {
            dao = _dao;
        }

        public bool CreateUserRole(int userId, int roleId)
        {
            return dao.CreateUserRole(userId, roleId);
        }

        public UserRole GetUserRole(int userId)
        {
            return dao.GetUserRole(userId);
        }
    }
}