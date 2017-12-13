using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Api.Rating;
using System.Collections.Generic;

namespace RiderQc.Web.Repository.Interface
{
    public interface IRatingRepository
    {
        float GetUserRatingByUserId(int userId);
        float GetRideRatingByRideId(int rideId);
        bool RateUser(UserRating rateUser);
        bool RateRide(RideRating rateRide);
        List<UserRatingViewModel> GetUserRatings(int userId);
        List<RideRatingViewModel> GetRideRatings(int rideId);
    }
}
