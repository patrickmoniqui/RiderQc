using RiderQc.Web.App_Start;
using RiderQc.Web.Entities;
using RiderQc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("trajet")]
    public class TrajetController : ApiController
    {
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
    }
}
