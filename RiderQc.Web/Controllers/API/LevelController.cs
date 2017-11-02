using RiderQc.Web.App_Start;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Level;
using System.Collections.Generic;
using System.Web.Http;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("level")]
    public class LevelController : ApiController
    {
        private readonly ILevelRepository repo;

        public LevelController(ILevelRepository _repo)
        {
            repo = _repo;
        }


        /// <summary>
        /// Create a level.
        /// </summary>
        /// <param name="levelViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("")]
        [AuthTokenAuthorization]
        public IHttpActionResult Create(LevelCreateViewModel levelViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = repo.Create(levelViewModel);

            if(result)
            {
                return Ok("Level successfully created.");
            }
            else
            {
                return BadRequest("Error while creating Level.");
            }
        }

        /// <summary>
        /// Get list of levels.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        public IHttpActionResult List()
        {
            List<LevelViewModel> levels = repo.GetLevelList();

            return Ok(levels);
        }
    }
}
