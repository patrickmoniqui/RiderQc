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
        double GetUserRatingByUserId(int userId);
        bool RateUser(UserRating userRating);
        bool RateRide(RideRating rideRating);
    }
}
