using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using System.Collections.Generic;
using RiderQc.Web.ViewModels.Ride;
using System;
using RiderQc.Web.Helpers;

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
            RideViewModel rideViewModel = dao.Get(rideId);
            return rideViewModel;
        }

        public List<RideViewModel> GetAllRides()
        {
            List<RideViewModel> rides = dao.GetAllRides();

            return rides;
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