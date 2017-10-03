using Newtonsoft.Json;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Ride;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

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
        /// Create a trajet
        /// </summary>
        /// <param name="trajetViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(TrajetViewModel trajetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = repo.Create(trajetViewModel);

            if (result)
            {
                return Ok("trajet successfully created.");
            }
            else
            {
                return BadRequest("Error while creating trajet.");
            }
        }

        /// <summary>
        /// Delete a trajet
        /// </summary>
        /// <param name="trajetId">Id of the trajet.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{trajetId}")]
        public IHttpActionResult Delete(int trajetId)
        {
            bool result = repo.Delete(trajetId);

            if (result)
            {
                return Ok("trajet successfully deleted.");
            }
            else
            {
                return BadRequest("Error while creating trajet.");
            }
        }

        /// <summary>
        /// Display a trajet
        /// </summary>
        /// <param name="trajetId">Id of the trajet.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{trajetId}")]
        public IHttpActionResult Get(int trajetId)
        {
            Trajet trajet = repo.Get(trajetId);

            if (trajet != null)
            {
                return Ok(trajet);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// List all trajets
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(List<Trajet>))]
        public IHttpActionResult GetAllTrajets()
        {
            List<Trajet> trajets = repo.GetAllTrajets();

            return Ok(trajets);
        }
    }
}
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
        [BasicAuthorization]
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
            List<TrajetViewModel> trajets = repo.GetTrajetList();

            return Ok(trajets);
        }
    }
}