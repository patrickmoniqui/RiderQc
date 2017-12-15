using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.ViewModels.Api.Rating
{
    public class UserRatingViewModel
    {
        public string Username { get; set; }
        public string RatingMessage { get; set; }
        public int Rate { get; set; }
    }
}