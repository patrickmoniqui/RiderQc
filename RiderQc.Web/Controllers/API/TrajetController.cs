using RiderQc.Web.App_Start;
using RiderQc.Web.Entities;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Trajet;
using System.Collections.Generic;
using System.Web.Http;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("trajet")]
    public class TrajetController : ApiController
    {
        private readonly ITrajetRepository repo;

        public TrajetController(ITrajetRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Create a trajet.
        /// </summary>
        /// <param name="trajet"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(Trajet trajet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)User;
            trajet.CreatorId = user.Id;

            bool result = false;

            if (result)
            {
                return Ok("Trajet successfully created.");
            }
            else
            {
                return BadRequest("Error while creating trajet.");
            }
        }

        /// <summary>
        /// List of trajets.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IHttpActionResult List()
        {
            List<TrajetViewModel> trajets = repo.GetAllTrajets();

            return Ok(trajets);
        }
    }
}
