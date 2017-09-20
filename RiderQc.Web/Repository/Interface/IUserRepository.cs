using RiderQc.Web.ViewModels.User;

namespace RiderQc.Web.Repository.Interface
{
    public interface IUserRepository
    {
        bool RegisterUser(UserRegisterViewModel user);
        bool DeleteUser(string username);
    }
}
