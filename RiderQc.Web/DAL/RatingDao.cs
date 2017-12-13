using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;

namespace RiderQc.Web.DAL
{
    public class RatingDao : IRatingDao
    {
        public float GetUserRatingByUserId(int userId)
        {
            using(RiderQcContext ctx = new RiderQcContext())
            {
                float rating = 0;
                User user = ctx.Users.Where(x => x.UserID == userId)
                    .Include(x => x.UserRatings)
                    .FirstOrDefault();
                if(user != null)
                {
                    if(user.UserRatings.Count > 0)
                    {
                        foreach (UserRating u in user.UserRatings)
                        {
                            rating += u.Rate;
                        }
                        return (rating / user.UserRatings.Count);
                    }
                }
                return rating;
            }
        }

        public float GetRideRatingByRideId(int rideId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                float rating = 0;
                Ride ride = ctx.Rides.Where(x => x.RideId == rideId)
                    .Include(x => x.RideRatings)
                    .FirstOrDefault();
                if (ride != null)
                {
                    if (ride.RideRatings.Count > 0)
                    {
                        foreach (RideRating r in ride.RideRatings)
                        {
                            rating += r.Rate;
                        }
                        return (rating / ride.RideRatings.Count);
                    }
                }
                return rating;
            }
        }

        public bool RateUser(UserRating userRating)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                int result;
                ctx.UserRatings.Add(userRating);
                result = ctx.SaveChanges();

                return result >= 1 ? true : false;
            }
        }

        public bool RateRide(RideRating rideRating)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                int result;
                ctx.RideRatings.Add(rideRating);
                result = ctx.SaveChanges();

                return result >= 1 ? true : false;
            }
        }

        public List<UserRating> GetAllUserRatings(int userId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                List<UserRating> ratings;
                ratings = ctx.UserRatings.Where(x => x.RatedId == userId).Include(x => x.User1).ToList();

                if (ratings != null)
                {
                    return ratings;
                }
                return new List<UserRating>();
            }
        }

        public List<RideRating> GetAllRideRatings(int rideId)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                List<RideRating> ratings;
                ratings = ctx.RideRatings.Where(x => x.RatedRideId == rideId).Include(x => x.User).ToList();

                if (ratings != null)
                {
                    return ratings;
                }
                return new List<RideRating>();
            }
        }
    }
}