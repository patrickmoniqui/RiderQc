using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using System.Collections.Generic;
using RiderQc.Web.ViewModels.Ride;
using System;

namespace RiderQc.Web.Repository
{
    public class RideRepository : IRideRepository
    {
        private readonly IRideDao dao;

        public RideRepository(IRideDao _dao)
        {
            dao = _dao;
        }

        public bool Create(RideViewModel rideViewModel)
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

        public bool Delete(int rideId)
        {
            return dao.Delete(rideId);
        }

        public RideViewModel Get(int rideId)
        {
            RideViewModel rideViewModel = new RideViewModel();
            Ride ride = dao.Get(rideId);
            rideViewModel.CreatorId = ride.CreatorId;
            rideViewModel.DateDepart = ride.DateDepart;
            rideViewModel.DateFin = ride.DateFin;
            rideViewModel.Description = ride.Description;
            rideViewModel.LevelId = ride.LevelId;
            rideViewModel.Title = ride.Title;
            rideViewModel.TrajetId = ride.TrajetId;
            return rideViewModel;
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
                rideViewModel.Comments = ride.Comments;
                rideViewModel.Level = ride.Level;
                rideViewModel.User = ride.User;
                rideViewModel.Trajet = ride.Trajet;
                list.Add(rideViewModel);
            }
            return list;
        }

        public bool UserIsCreator(int rideId, string username)
        {
            if(rideId <= 0 || string.IsNullOrWhiteSpace(username))
            {
                return false;
            }
            return dao.UserIsCreator(rideId, username);
        }
    }
}