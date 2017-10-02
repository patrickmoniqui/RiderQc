using RiderQc.Web.Entities;
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
        [HttpPost]
        [Route("add")]
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
        [HttpPost]
        [Route("edit")]
        [ResponseType(typeof(MotoViewModel))]
        public IHttpActionResult EditMoto(MotoViewModel motoViewModel)
        {
            //NEEDS USER AUTHENTIFICATION

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool result = repo.EditMoto(motoViewModel);

            if (!result)
                return BadRequest("An error as occured.");

            return Ok("The moto changes were successful.");
        }

        /// <summary>
        /// Delete moto
        /// </summary>
        /// <param name="motoId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{motoId}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteMoto(int motoId)
        {
            //NEEDS USER AUTHENTIFICATION

            bool result = repo.DeleteMoto(motoId);

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
            //NEEDS USER AUTHENTIFICATION
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
            //NEEDS USER AUTHENTIFICATION
            return Ok(repo.GetAllMotos());
        }
    }
}