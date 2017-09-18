using Newtonsoft.Json;
using RiderQc.Web.DAL;
using RiderQc.Web.Repository;
using System.Collections.Generic;
using System.Web.Http;

namespace RiderQc.Web.Controllers.Api
{
    [RoutePrefix("ride")]
    public class RideController : ApiController
    {
        private readonly IRideRepository repo;

        public RideController(/*IRideRepository _repo*/)
        {
            repo = new RideRepository();
        }

        [System.Web.Http.AcceptVerbs("GET")]
        [System.Web.Http.HttpGet]
        public IHttpActionResult GetAllRides()
        {
            List<Ride> rides = repo.GetAllRides();

            string json = JsonConvert.SerializeObject(rides, Formatting.None, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            
            return Ok(json);
        }
    }
}
