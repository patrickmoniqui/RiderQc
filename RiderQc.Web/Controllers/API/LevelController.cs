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
