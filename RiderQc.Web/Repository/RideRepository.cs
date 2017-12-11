using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using System.Collections.Generic;
using RiderQc.Web.ViewModels.Ride;

namespace RiderQc.Web.Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly IRideDao dao;

        public RideRepository(IRideDao _dao)
        {
            dao = _dao;
        }
        
        public bool Create(RideCreateViewModel rideViewModel)
        {
            //mapping
            Ride ride = new Ride();
            ride.Title = rideViewModel.Title;
            ride.Description = rideViewModel.Description;
            ride.CreatorId = rideViewModel.CreatorId;
            ride.TrajetId = rideViewModel.TrajetId;
            ride.LevelId = rideViewModel.LevelId;
            ride.DateDepart = rideViewModel.DateDepart;
            ride.DateFin = rideViewModel.DateFin;

            return dao.Create(ride);
        }

        public bool CreateWithTrajet(RideCreateWithTrajetViewModel rideViewModel)
        {
            //mapping
            Trajet trajet = new Trajet();
            trajet.Title = rideViewModel.Trajet.Title;
            trajet.Description = rideViewModel.Trajet.Description;
            trajet.CreatorId = rideViewModel.Trajet.CreatorId;
            string googleCo = string.Join(";", rideViewModel.Trajet.GpsPoints);
            trajet.GoogleCo = googleCo;
            
            Ride ride = new Ride();
            ride.Title = rideViewModel.Title;
            ride.Description = rideViewModel.Description;
            ride.CreatorId = rideViewModel.CreatorId;
            ride.LevelId = rideViewModel.LevelId;
            ride.DateDepart = rideViewModel.DateDepart;
            ride.DateFin = rideViewModel.DateFin;
            ride.Trajet = trajet;

            return dao.Create(ride);
        }

        public bool Delete(int rideId)
        {
            return dao.Delete(rideId);
        }

        public bool Exist(int rideId)
        {
            return dao.Exist(rideId);
        }

        public RideViewModel Get(int rideId)
        {
            RideViewModel rideViewModel = dao.Get(rideId);
            return rideViewModel;
        }

        public List<RideViewModel> GetAllRides()
        {
            List<RideViewModel> rides = dao.GetAllRides();
                
            return rides;
        }

        public List<string> GetPartipants(int rideId)
        {
            return dao.GetPartipants(rideId);
        }

        public bool AddUserToParticipants(int rideId, string username)
        {
            return dao.AddUserToParticipants(rideId, username);
        }

        public bool RemoveUserToParticipants(int rideId, string username)
        {
            return dao.RemoveUserToParticipants(rideId, username);
        }

        public bool Update(RideCreateViewModel rideViewModel)
        {
            Ride ride = new Ride();
            ride.RideId = rideViewModel.RideId;
            ride.Title = rideViewModel.Title;
            ride.Description = rideViewModel.Description;
            ride.CreatorId = rideViewModel.CreatorId;
            ride.LevelId = rideViewModel.LevelId;
            ride.DateDepart = rideViewModel.DateDepart;
            ride.DateFin = rideViewModel.DateFin;

            return dao.Update(ride);
        }

        public bool UserIsCreator(int rideId, string username)
        {
            if(rideId <= 0 || string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            return dao.UserIsCreator(rideId, username);
        }

        public List<RideViewModel> MyRidesForUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            List<RideViewModel> ridesViewModel = dao.MyRidesForUser(username);

            return ridesViewModel;
        }
    }
}