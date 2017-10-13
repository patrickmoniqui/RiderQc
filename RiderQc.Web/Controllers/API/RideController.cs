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

        public RideController(IRideRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Create a ride
        /// </summary>
        /// <param name="rideViewModel"></param>
        /// <returns></returns>
        [BasicAuthorization]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(RideViewModel rideViewModel)
        {
            if(!ModelState.IsValid)
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
        /// Delete a ride
        /// </summary>
        /// <param name="rideId">Id of the ride.</param>
        [BasicAuthorization]
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
            
            return Ok(rides);
        }
    }
}
