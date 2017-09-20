using Newtonsoft.Json;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
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
