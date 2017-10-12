using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.Trajet;
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
            bool result = dao.DeleteUser(username);
            return result;
        }

        public bool RegisterUser(UserRegisterViewModel userViewModel)
        {
            //map viewmodel to entity
            User user = new User();
            user.UserID = userViewModel.UserID;
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

        public List<UserViewModel> GetAllUsers()
        {
            List<UserViewModel> list = new List<UserViewModel>();

            List<User> users = dao.GetAllUsers();

            foreach (User user in users)
            {
                UserViewModel userViewModel = new UserViewModel();
                userViewModel.UserID = user.UserID;
                userViewModel.Username = user.Username;
                userViewModel.Region = user.Region;
                userViewModel.DateOfBirth = user.DateOfBirth;
                userViewModel.Description = user.Description;
                userViewModel.DpUrl = user.DpUrl;
                userViewModel.Motos = user.Motoes;
                userViewModel.Rides = user.Rides;
                userViewModel.Ville = user.Ville;
                list.Add(userViewModel);
            }

            return list;
        }

        public List<RideViewModel> GetAllRides()
        {
            List<RideViewModel> list = new List<RideViewModel>();
            List<Ride> rides = dao.GetAllRides();

            foreach (Ride ride in rides)
            {
                RideViewModel rideViewModel = new RideViewModel();
                rideViewModel.CreatorId = ride.CreatorId;
                rideViewModel.DateDepart = ride.DateDepart;
                rideViewModel.DateFin = ride.DateFin;
                rideViewModel.Description = ride.Description;
                rideViewModel.LevelId = ride.LevelId;
                rideViewModel.Title = ride.Title;
                rideViewModel.TrajetId = ride.TrajetId;
                list.Add(rideViewModel);
            }
            return list;
        }

        public List<TrajetViewModel> GetAllTrajets()
        {
            {
                List<TrajetViewModel> list = new List<TrajetViewModel>();

                List<Trajet> trajets = dao.GetAllTrajets();

                foreach (Trajet trajet in trajets)
                {
                    TrajetViewModel trajetViewModel = new TrajetViewModel();
                    trajetViewModel.TrajetId = trajet.TrajetId;
                    trajetViewModel.GpsPoints = new List<string>();

                    var gpsPoints = trajet.GoogleCo.Split(';');

                    trajetViewModel.GpsPoints.AddRange(gpsPoints);

                    list.Add(trajetViewModel);
                }

                return list;
            }
        }

        public UserViewModel GetUserById(int userId)
        {
            UserViewModel userViewModel = new UserViewModel();
            User user = dao.GetUserById(userId);
            userViewModel.UserID = user.UserID;
            userViewModel.Username = user.Username;
            userViewModel.Region = user.Region;
            userViewModel.DateOfBirth = user.DateOfBirth;
            userViewModel.Description = user.Description;
            userViewModel.DpUrl = user.DpUrl;
            userViewModel.Motos = user.Motoes;
            userViewModel.Rides = user.Rides;
            userViewModel.Ville = user.Ville;
            return userViewModel;
        }

        
    }
}