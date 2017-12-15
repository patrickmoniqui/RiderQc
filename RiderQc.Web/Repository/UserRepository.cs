using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Admin;
using RiderQc.Web.ViewModels.Api.User;
using RiderQc.Web.ViewModels.Moto;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;

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
            bool result = dao.Delete(username);
            return result;
        }

        public bool RegisterUser(UserRegisterViewModel userViewModel)
        {
            if(dao.CheckUserExistence(userViewModel.Username))
            {
                return false;
            }
            
            //map viewmodel to entity
            User user = new User();
            user.Username = userViewModel.Username;
            user.Password = userViewModel.Password;
            user.Region = userViewModel.Region;
            user.Ville = userViewModel.Ville;
            user.DateOfBirth = userViewModel.DateOfBirth;
            user.Description = userViewModel.Description;
            user.DpUrl = userViewModel.DpUrl;

            return dao.RegisterUser(user);
        }

        public bool CheckUserExistence(string username)
        {
            return dao.CheckUserExistence(username);
        }

        public List<UserViewModel> GetAllUsers()
        {
            List<User> users = dao.GetAllUsers();
            List<UserViewModel> usersViewModel = new List<UserViewModel>();

            users.ForEach(x => usersViewModel.Add(ToViewModel(x)));
            
            return usersViewModel;
        }
        
        public UserViewModel GetUserById(int userId)
        {
            User user = dao.GetUserById(userId);

            return user != null ? ToViewModel(user) : null;
        }

        public UserViewModel GetUserByName(string username)
        {
            User user = dao.GetByUsername(username);

            return user != null ? ToViewModel(user) : null;
        }

        public bool CredentialsAreValid(string username, string password)
        {
            return dao.CredentialsAreValid(username, password);
        }

        public AuthentificationTokenViewModel GenerateTokenForUser(string username, int expiresAfterNbDays = 30)
        {
            return dao.GenerateTokenForUser(username);
        }

        public AuthentificationTokenViewModel GetLastValidTokenByUsername(string username)
        {
            return dao.GetLastValidTokenByUsername(username);
        }

        public UserViewModel GetUserByTokenIfLastTokenIsValid(string token)
        {
            User user = dao.GetUserByTokenIsLastTokenIsValid(token);

            if (user == null)
            {
                return null;
            }
            else
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.UserID = user.UserID;
                userViewModel.Username = user.Username;
                userViewModel.Region = user.Region;
                userViewModel.Ville = user.Ville;
                userViewModel.DateOfBirth = user.DateOfBirth;
                userViewModel.Description = user.Description;
                userViewModel.DpUrl = user.DpUrl;

                return userViewModel;
            }
        }

        public List<RideViewModel> GetMyRides(string username)
        {
            if(string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            List<Ride> rides = dao.GetMyRides(username);
            List<RideViewModel> ridesViewModel = new List<RideViewModel>();

            foreach(Ride ride in rides)
            {
                RideViewModel rideViewModel = new RideViewModel();
                rideViewModel.RideId = ride.RideId;
                rideViewModel.Title = ride.Title;
                rideViewModel.Description = ride.Description;
                rideViewModel.CreatorId = ride.CreatorId;
                rideViewModel.TrajetId = ride.TrajetId;
                rideViewModel.DateDepart = ride.DateDepart;
                rideViewModel.DateFin = ride.DateFin;

                ridesViewModel.Add(rideViewModel);
            }

            return ridesViewModel;
        }

        private UserViewModel ToViewModel(User user)
        {
            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserID = user.UserID;
            userViewModel.Username = user.Username;
            userViewModel.Region = user.Region;
            userViewModel.Ville = user.Ville;
            userViewModel.DateOfBirth = user.DateOfBirth;
            userViewModel.Description = user.Description;
            userViewModel.DpUrl = user.DpUrl;

            List<MotoViewModel> motosViewModel = new List<MotoViewModel>();

            if(user.Motoes != null)
            {
                foreach (Moto moto in user.Motoes)
                {
                    MotoViewModel motoViewModel = new MotoViewModel();
                    motoViewModel.MotoId = moto.MotoId;
                    motoViewModel.Brand = moto.Brand;
                    motoViewModel.Model = moto.Model;
                    motoViewModel.Type = moto.Type;
                    motoViewModel.Year = moto.Year;
                    motoViewModel.UserId = moto.UserId;

                    motosViewModel.Add(motoViewModel);
                }
            }
            
            userViewModel.Motos = motosViewModel;
            
            return userViewModel;
        }

        private UserAdminViewModel ToAdminViewModel(User user)
        {
            UserAdminViewModel userViewModel = new UserAdminViewModel();
            userViewModel.UserID = user.UserID;
            userViewModel.Username = user.Username;
            userViewModel.Password = user.Password;
            userViewModel.Region = user.Region;
            userViewModel.Ville = user.Ville;
            userViewModel.DateOfBirth = user.DateOfBirth;
            userViewModel.Description = user.Description;
            userViewModel.DpUrl = user.DpUrl;

            List<MotoViewModel> motosViewModel = new List<MotoViewModel>();

            if (user.Motoes != null)
            {
                foreach (Moto moto in user.Motoes)
                {
                    MotoViewModel motoViewModel = new MotoViewModel();
                    motoViewModel.MotoId = moto.MotoId;
                    motoViewModel.Brand = moto.Brand;
                    motoViewModel.Model = moto.Model;
                    motoViewModel.Type = moto.Type;
                    motoViewModel.Year = moto.Year;
                    motoViewModel.UserId = moto.UserId;

                    motosViewModel.Add(motoViewModel);
                }
            }

            userViewModel.Motos = motosViewModel;

            return userViewModel;
        }

        public UserAdminViewModel GetUserAdminById(int userId)
        {
            User user = dao.GetUserById(userId);

            return user != null ? ToAdminViewModel(user) : null;
        }

        public User GetUserByNameAdmin(string username)
        {
            User user = dao.GetByUsername(username);
            return user;
        }

        public bool EditUser(UserViewModel userViewModel)
        {
            User user = new User();
            user.DateOfBirth = userViewModel.DateOfBirth;
            user.Description = userViewModel.Description;
            user.DpUrl = userViewModel.DpUrl;
            user.Region = userViewModel.Region;
            user.Ville = userViewModel.Ville;
            
            return dao.EditUser(user);
        }

        public User GetRawUserById(int userId)
        {
            return dao.GetUserById(userId);
        }
    }
}