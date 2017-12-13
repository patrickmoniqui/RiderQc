using RiderQc.Web.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Api.Rating;
using RiderQc.Web.DAL.Interface;

namespace RiderQc.Web.Repository
{
    public class RatingRepository : IRatingRepository
    {
        private readonly IRatingDao dao;

        public RatingRepository(IRatingDao _dao)
        {
            dao = _dao;
        }

        public float GetUserRatingByUserId(int userId)
        {
            return dao.GetUserRatingByUserId(userId);
        }

        public List<UserRatingViewModel> GetUserRatings(int userId)
        {
            List<UserRatingViewModel> ratings = new List<UserRatingViewModel>();

            foreach (UserRating r in dao.GetAllUserRatings(userId))
            {
                UserRatingViewModel rating = new UserRatingViewModel();
                rating.Username = r.User.Username;
                rating.RatingMessage = r.RatingMessage;
                rating.Rate = r.Rate;
                ratings.Add(rating);
            }

            return ratings;
        }

        public List<RideRatingViewModel> GetRideRatings(int rideId)
        {
            List<RideRatingViewModel> ratings = new List<RideRatingViewModel>();

            foreach (RideRating r in dao.GetAllRideRatings(rideId))
            {
                RideRatingViewModel rating = new RideRatingViewModel();
                rating.Username = r.User.Username;
                rating.RatingMessage = r.RatingMessage;
                rating.Rate = r.Rate;
                ratings.Add(rating);
            }

            return ratings;
        }

        public bool RateRide(RideRating rateRide)
        {
            return dao.RateRide(rateRide);
        }

        public bool RateUser(UserRating rateUser)
        {
            return dao.RateUser(rateUser);
        }

        public float GetRideRatingByRideId(int rideId)
        {
            return dao.GetRideRatingByRideId(rideId);
        }
    }
}