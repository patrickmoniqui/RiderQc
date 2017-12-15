using RiderQc.Web.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiderQc.Web.DAL.Interface
{
    public interface IRatingDao
    {
        float GetUserRatingByUserId(int userId);
        float GetRideRatingByRideId(int rideId);
        bool RateUser(UserRating userRating);
        bool RateRide(RideRating rideRating);
        List<UserRating> GetAllUserRatings(int userId);
        List<RideRating> GetAllRideRatings(int rideId);
    }
}
