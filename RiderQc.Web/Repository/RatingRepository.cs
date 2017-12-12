using RiderQc.Web.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RiderQc.Web.Entities;

namespace RiderQc.Web.Repository
{
    public class RatingRepository : IRatingRepository
    {
        public UserRating GetUserRatingByUserId(int userId)
        {
            throw new NotImplementedException();
        }
    }
}