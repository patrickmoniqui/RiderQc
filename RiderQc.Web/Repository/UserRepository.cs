using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.User;

namespace RiderQc.Web.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly IUserDao dao;

        public UserRepository(IUserDao _dao)
        {
            dao = _dao;
        }

        public bool DeleteUser(string username)
        {
            bool result = dao.DeleteUser(username);
            return result;
        }

        public bool RegisterUser(UserRegisterViewModel userViewModel)
        {
            //map viewmodel to entity
            User user = new User();

            user.Username = userViewModel.Username;
            user.Password = userViewModel.Password;
            user.Region = userViewModel.Region;
            user.Ville = userViewModel.Ville;
            user.DateOfBirth = userViewModel.DateOfBirth;
            user.DpUrl = userViewModel.DpUrl;

            bool result = dao.RegisterUser(user);

            return result;
        }

        public bool CheckUserExistence(string username)
        {
            return dao.CheckUserExistence(username);
        }
    }
}