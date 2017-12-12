using RiderQc.Web.DAL.Interface;
using RiderQc.Web.Entities;
using System.Linq;
using System.Data.Entity;

namespace RiderQc.Web.DAL
{
    public class RatingDao : IRatingDao
    {
        public double GetUserRatingByUserId(int userId)
        {
            using(RiderQcContext ctx = new RiderQcContext())
            {
                int rating = 0;
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
                    }
                    return (rating/5);
                }
                return -1;
            }
        }

        public bool RateUser(UserRating userRating)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                int result = -1;
                ctx.UserRatings.Add(userRating);
                result = ctx.SaveChanges();

                if(result != -1)
                {
                    return true;
                }
                return false;
            }
        }

        public bool RateRide(RideRating rideRating)
        {
            using (RiderQcContext ctx = new RiderQcContext())
            {
                //int result = -1;
                //ctx.UserRatings.Add(userRating);
                //result = ctx.SaveChanges();

                //if (result != -1)
                //{
                //    return true;
                //}
                return false;
            }
        }
        
    }
}