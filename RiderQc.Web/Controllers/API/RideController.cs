using RiderQc.Web.Entities;
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
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(RideViewModel rideViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

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
        /// <returns></returns>
        [HttpDelete]
        [Route("{rideId}")]
        public IHttpActionResult Delete(int rideId)
        {
            bool result = repo.Delete(rideId);

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
            Ride ride = repo.Get(rideId);

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
        [ResponseType(typeof(List<Ride>))]
        public IHttpActionResult GetAllRides()
        {
            List<Ride> rides = repo.GetAllRides();
            
            return Ok(rides);
        }
    }
}
