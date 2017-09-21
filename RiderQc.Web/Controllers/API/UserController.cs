using Newtonsoft.Json;
using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.User;
using Swashbuckle.Swagger.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository repo;

        public UserController(IUserRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ResponseType(typeof(UserRegisterViewModel))]
        public IHttpActionResult Register(UserRegisterViewModel userViewModel)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool result = repo.RegisterUser(userViewModel);
            
            if(result)
            {
                return Ok("User successfully registered!");
            }
            else
            {
                return BadRequest("Error while register user.");
            }
        }

        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="username">username of the user.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{username}")]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Please enter a valid username.");
            }

            bool result = repo.DeleteUser(username);

            if (result)
            {
                return Ok("User successfully deleted!");
            }
            else
            {
                return BadRequest("Error while deleting user.");
            }
        }

        /// <summary>
        /// List all the users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(List<User>))]
        public IHttpActionResult GetAllUsers()
        {
            List<User> users = new List<User>();

            using (RiderQcContext ctx = new RiderQcContext())
            {
                users = ctx.Users.ToList();
            }

            return Ok(users);
        }
    }
}
