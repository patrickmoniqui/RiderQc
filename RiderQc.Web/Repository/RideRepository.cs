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

        public Ride Get(int rideId)
        {
            return dao.Get(rideId);
        }

        public List<Ride> GetAllRides()
        {
            return dao.GetAllRides();
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