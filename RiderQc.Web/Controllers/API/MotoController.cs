using RiderQc.Web.App_Start;
using RiderQc.Web.Entities;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Moto;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("moto")]
    public class MotoController : ApiController
    {
        private readonly IMotoRepository repo;

        public MotoController(IMotoRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Create/Add moto
        /// </summary>
        /// <param name="motoViewModel"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(MotoViewModel))]
        public IHttpActionResult CreateMoto(MotoViewModel motoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = repo.CreateMoto(motoViewModel);

            if (!result)
                return BadRequest("An error as occured.");

            return Ok("A moto has been successfully created.");
        }

        /// <summary>
        /// Edit moto
        /// </summary>
        /// <param name="motoViewModel"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(MotoViewModel))]
        public IHttpActionResult EditMoto(MotoViewModel motoViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            ApplicationUser user = (ApplicationUser)User;

            bool result = false;

            if (repo.UserHasAccess(motoViewModel.MotoId, user.Username))
            {
                result = repo.EditMoto(motoViewModel);
            }
            else
            {
                return Unauthorized();
            }

            if (!result)
                return BadRequest("An error as occured.");

            return Ok("The moto changes were successful.");
        }

        /// <summary>
        /// Delete moto
        /// </summary>
        /// <param name="motoId"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpDelete]
        [Route("{motoId}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteMoto(int motoId)
        {
            ApplicationUser user = (ApplicationUser)User;
        
            bool result = false;

            if (repo.UserHasAccess(motoId, user.Username))
            {
                result = repo.DeleteMoto(motoId);
            }
            else
            {
                return Unauthorized();
            }

            if (!result)
                return BadRequest("An error as occured.");

            return Ok("The moto was successfully deleted.");
        }

        /// <summary>
        /// Get moto
        /// </summary>
        /// <param name="motoId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{motoId}")]
        [ResponseType(typeof(int))]
        public IHttpActionResult GetMoto(int motoId)
        {
            Moto moto = repo.GetMoto(motoId);

            if (moto == null)
                return BadRequest("Moto was not found.");

            return Ok(moto);
        }

        /// <summary>
        /// Edit moto
        /// </summary>
        /// <param name="motoViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(List<Moto>))]
        public IHttpActionResult GetAllMotos()
        {
            return Ok(repo.GetAllMotos());
        }
    }
}