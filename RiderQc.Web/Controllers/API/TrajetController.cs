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
        /// Get a Trajet.
        /// </summary>
        /// <param name="trajetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{trajetId}")]
        public IHttpActionResult Get(int trajetId)
        {
            TrajetViewModel trajetViewModel = null;

            if(trajetId <= 0 || !repo.Exist(trajetId))
            {
                ModelState.AddModelError("trajetId", "Trajet is not valid.");
            }

            trajetViewModel = repo.Get(trajetId);

            if(trajetViewModel != null)
            {
                return Ok(trajetViewModel);
            }
            else
            {
                return NotFound();  
            }
        }

        /// <summary>
        /// Create a trajet.
        /// </summary>
        /// <param name="trajetViewModel"></param>
        /// <param name="trajet"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("")]
        public IHttpActionResult Create(TrajetCreateViewModel trajetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)User;
            trajetViewModel.CreatorId = user.Id;

            bool result = repo.Create(trajetViewModel);

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
        /// Update a trajet.
        /// </summary>
        /// <param name="trajetId"></param>
        /// <param name="trajetViewModel"></param>
        /// <param name="trajet"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPut]
        [Route("{trajetId}")]
        public IHttpActionResult Update(int trajetId, TrajetCreateViewModel trajetViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationUser user = (ApplicationUser)User;
            trajetViewModel.CreatorId = user.Id;
            trajetViewModel.TrajetId = trajetId;

            bool result = repo.Update(trajetViewModel);

            if (result)
            {
                return Ok("Trajet successfully updated.");
            }
            else
            {
                return BadRequest("Error while updating trajet.");
            }
        }

        /// <summary>
        /// Delete a Trajet.
        /// </summary>
        /// <param name="trajetId"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpDelete]
        [Route("{trajetId}")]
        public IHttpActionResult Delete(int trajetId)
        {
            bool result = repo.Delete(trajetId);

            if (result)
            {
                return Ok("Trajet successfully deleted!");
            }
            else
            {
                return BadRequest("Error while deleting Trajet.");
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
