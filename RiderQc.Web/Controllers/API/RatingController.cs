using RiderQc.Web.ViewModels.Api.Rating;
using RiderQc.Web.App_Start;
using RiderQc.Web.Repository.Interface;
using System.Web.Http;
using System.Web.Http.Description;
using RiderQc.Web.Models;
using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.User;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("rating")]
    public class RatingController : ApiController
    {
        private readonly IRatingRepository repo;
        private readonly IUserRepository repo2;

        public RatingController(IRatingRepository _repo, IUserRepository _repo2)
        {
            repo = _repo;
            repo2 = _repo2;
        }

        [AuthTokenAuthorization]
        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("ride/{rideId}/{ratingNb}")]
        public IHttpActionResult RateRide(int rideId, int ratingNb)
        {
            if (rideId == 0 || (ratingNb < 0 || ratingNb > 5))
                return BadRequest("Invalid parameters (rideId, ratingNb(between 0-5))");

            ApplicationUser user = (ApplicationUser)User;

            RideRating rRating = new RideRating();
            rRating.Rate = ratingNb;
            rRating.RaterId = user.Id;
            rRating.RatedRideId = rideId;
            rRating.RatingMessage = "";

            bool result = repo.RateRide(rRating);

            if (!result)
                return BadRequest("An error as occured.");

            return Ok("You have successfully rated this ride.");
        }

        [AuthTokenAuthorization]
        [HttpPost]
        [ResponseType(typeof(string))]
        [Route("user/{username}/{ratingNb}")]
        public IHttpActionResult RateUser(string username, int ratingNb)
        {
            if (string.IsNullOrEmpty(username) || (ratingNb < 0 || ratingNb > 5))
                return BadRequest("Invalid parameters (username, ratingNb(between 0-5))");

            UserViewModel ratedUser = repo2.GetUserByName(username);
            ApplicationUser user = (ApplicationUser)User;

            if (ratedUser == null || ratedUser.UserID == user.Id)
                return BadRequest("Invalid user to rate/Cannot rate yourself.");

            UserRating uRating = new UserRating();
            uRating.Rate = ratingNb;
            uRating.RaterId = user.Id;
            uRating.RatedId = ratedUser.UserID;
            uRating.RatingMessage = "";

            bool result = repo.RateUser(uRating);

            if (!result)
                return BadRequest("An error as occured.");

            return Ok("You have successfully rated this user.");
        }

        [AuthTokenAuthorization]
        [HttpGet]
        [ResponseType(typeof(float))]
        [Route("user/{userId}")]
        public IHttpActionResult GetUserRating(int userId)
        {
            return Ok(repo.GetUserRatingByUserId(userId));
        }

        [AuthTokenAuthorization]
        [HttpGet]
        [ResponseType(typeof(float))]
        [Route("ride/{rideId}")]
        public IHttpActionResult GetRideRating(int rideId)
        {
            return Ok(repo.GetRideRatingByRideId(rideId));
        }

    }
}
