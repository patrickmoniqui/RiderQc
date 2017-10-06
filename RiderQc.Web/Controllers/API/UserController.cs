using RiderQc.Web.App_Start;
using RiderQc.Web.Entities;
using RiderQc.Web.Helpers;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.User;
using System;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!EncryptionHelper.IsBase64(userViewModel.Username) || !(EncryptionHelper.IsBase64(userViewModel.Password)))
                return BadRequest("An error has occured");


            string username = EncryptionHelper.Base64Decode(userViewModel.Username);

            if (repo.CheckUserExistence(username))
                return BadRequest("That username is already taken.");

            userViewModel.Username = username;

            string password = EncryptionHelper.Base64Decode(userViewModel.Password);

            userViewModel.Password = EncryptionHelper.HashToSHA256(password);

            DateTime? userDOB = userViewModel.DateOfBirth;
            string region = userViewModel.Region;
            string ville = userViewModel.Ville;
            string dpURL = userViewModel.DpUrl;

            bool result = repo.RegisterUser(userViewModel);

            if (result)
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
        [BasicAuthorization]

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
