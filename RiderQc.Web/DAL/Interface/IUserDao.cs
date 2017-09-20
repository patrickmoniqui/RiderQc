using RiderQc.Web.Entities;

namespace RiderQc.Web.DAL.Interface
{
    public interface IUserDao
    {
        bool RegisterUser(User user);
        bool DeleteUser(string username);
    }
}
