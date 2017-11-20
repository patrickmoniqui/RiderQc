using RiderQc.Web.App_Start;
using RiderQc.Web.Entities;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace RiderQc.Web.Controllers.Api
{
    [RoutePrefix("ride")]
    public class RideController : ApiController
    {
        private readonly IRideRepository repo;
        private readonly IUserRepository userRepo;
        private readonly ITrajetRepository trajetRepo;
        private readonly ILevelRepository levelRepo;

        public RideController(IRideRepository _repo, IUserRepository _userRepo, ITrajetRepository _trajetRepo, ILevelRepository _levelRepo)
        {
            repo = _repo;
            userRepo = _userRepo;
            trajetRepo = _trajetRepo;
            levelRepo = _levelRepo;
        }

        /// <summary>
        /// Create a ride
        /// </summary>
        /// <param name="rideViewModel"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(RideCreateViewModel rideViewModel)
        {
            ValidateLevel(rideViewModel.LevelId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)User;
            rideViewModel.CreatorId = user.Id;

            bool result = repo.Create(rideViewModel);

            if(result)
            {
                return Ok("Ride successfully created.");
            }
            else
            {
                return BadRequest("Error while creating ride.");
            }
        }

        /// <summary>
        /// Create a ride with new trajet
        /// </summary>
        /// <param name="rideViewModel"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("withtrajet")]
        public IHttpActionResult CreateWithTrajet(RideCreateWithTrajetViewModel rideViewModel)
        {
            ValidateLevel(rideViewModel.LevelId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)User;
            rideViewModel.CreatorId = user.Id;
            rideViewModel.Trajet.CreatorId = user.Id;
            
            bool result = repo.CreateWithTrajet(rideViewModel);

            if (result)
            {
                return Ok("Ride and Trajet successfully created.");
            }
            else
            {
                return BadRequest("Error while creating ride and trajet.");
            }
        }

        /// <summary>
        /// Update a Ride.
        /// </summary>
        /// <param name="rideViewModel"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{rideId}")]
        [AuthTokenAuthorization]
        public IHttpActionResult Update(int rideId, RideCreateViewModel rideViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = repo.Update(rideViewModel);

            if(result)
            {
                return Ok("Ride sucessfully updated");
            }
            else
            {
                return BadRequest();
            }

        }

        /// <summary>
        /// Delete a ride
        /// </summary>
        /// <param name="rideId">Id of the ride.</param>
        [AuthTokenAuthorization]
        [HttpDelete]
        [Route("{rideId}")]
        public IHttpActionResult Delete(int rideId)
        {
            ApplicationUser user = (ApplicationUser)User;

            bool result = false;

            if(repo.UserIsCreator(rideId, user.Username))
            {
                result = repo.Delete(rideId);
            }
            else
            {
                return Unauthorized();
            }
            
            if (result)
            {
                return Ok("Ride successfully deleted.");
            }
            else
            {
                return BadRequest("Error while creating ride.");
            }
        }

        /// <summary>
        /// Display a ride
        /// </summary>
        /// <param name="rideId">Id of the ride.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{rideId}")]
        public IHttpActionResult Get(int rideId)
        {
            RideViewModel ride = repo.Get(rideId);

            if(ride != null)
            {
                return Ok(ride);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// List all rides
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(List<RideViewModel>))]
        public IHttpActionResult GetAllRides()
        {
            List<RideViewModel> rides = repo.GetAllRides();

            if (rides?.Count == 0)
            {
                return NotFound();
            }
            else if (rides.Count > 0)
            {
                return Ok(rides);
            }
            else
            {
                return BadRequest();
            }
        }
        
        /// <summary>
        /// Get attending rides of user.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("myrides")]
        [ResponseType(typeof(List<RideViewModel>))]
        public IHttpActionResult GetMyRidesForUser([FromUri] string username)
        {
            if(string.IsNullOrWhiteSpace(username))
            {
                ModelState.AddModelError("username", "username is required.");
                return BadRequest(ModelState);
            }

            List<RideViewModel> rides = repo.MyRidesForUser(username);

            if (rides?.Count == 0)
            {
                return NotFound();
            }
            else if (rides.Count > 0)
            {
                return Ok(rides);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Participate to a ride
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{rideId}/participate")]
        [AuthTokenAuthorization]
        [ResponseType(typeof(bool))]
        public IHttpActionResult Participate(int rideId)
        {
            ApplicationUser user = (ApplicationUser)User;

            if(!repo.Exist(rideId))
            {
                ModelState.AddModelError("rideId", "Ride is not valid.");
            }

            if (!userRepo.CheckUserExistence(user.Username))
            {
                ModelState.AddModelError("username", "User is not valid.");
            }
            
            if (!ModelState.IsValid)
            {
                return BadRequest("Error adding user to ride's participants.");
            }

            bool result = repo.AddUserToParticipants(rideId, user.Username);

            return Ok();
        }

        /// <summary>
        /// Remove participation to a ride
        /// </summary>
        /// <returns></returns>
        [HttpDelete]
        [Route("{rideId}/participate")]
        [AuthTokenAuthorization]
        [ResponseType(typeof(bool))]
        public IHttpActionResult RemoveParticipate(int rideId)
        {
            ApplicationUser user = (ApplicationUser)User;

            if (!repo.Exist(rideId))
            {
                ModelState.AddModelError("rideId", "Ride is not valid.");
            }

            if (!userRepo.CheckUserExistence(user.Username))
            {
                ModelState.AddModelError("username", "User is not valid.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Error removing user to ride's participants.");
            }

            bool result = repo.RemoveUserToParticipants(rideId, user.Username);

            return Ok();
        }

        /// <summary>
        /// Get list of partipants username for a Ride.
        /// </summary>
        /// <param name="rideId"></param>
        /// <returns></returns>

        [HttpGet]
        [Route("participant/list")]
        public IHttpActionResult GetParticipantsList(int rideId)
        {
            if(rideId <= 0 || !repo.Exist(rideId))
            {
                ModelState.AddModelError("rideId", "Ride is not valid.");
                return BadRequest();
            }

            List<string> participants = repo.GetPartipants(rideId);

            if(participants?.Count >= 0)
            {
                return Ok(participants);
            }
            else if(participants?.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return BadRequest();
            }
        }

        private bool ValidateLevel(int levelId)
        {
            if (levelId <= 0 || !levelRepo.Exist(levelId))
            {
                ModelState.AddModelError("LevelId", "Level is not valid.");
                return false;
            }
            return true;
        }
    }
}
