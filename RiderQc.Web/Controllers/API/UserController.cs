using RiderQc.Web.App_Start;
using RiderQc.Web.Helpers;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Api.User;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository repo;
        private readonly IUserRoleRepository repo2;

        public UserController(IUserRepository _repo, IUserRoleRepository _repo2)
        {
            repo = _repo;
            repo2 = _repo2;
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

            if (repo.CheckUserExistence(userViewModel.Username))
                return BadRequest("That username is already taken.");

            DateTime? userDOB = userViewModel.DateOfBirth;
            string region = userViewModel.Region;
            string ville = userViewModel.Ville;
            string dpURL = userViewModel.DpUrl;

            bool result = repo.RegisterUser(userViewModel);

            if (result)
            {
                result = repo2.CreateUserRole(
                    repo.GetUserByName(userViewModel.Username).UserID,
                    Constant.UserRole_User
                    );
                return Ok("User '" + userViewModel.Username +"' successfully registered!");
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
        [AuthTokenAuthorization]
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
        [ResponseType(typeof(List<UserViewModel>))]
        public IHttpActionResult GetAllUsers()
        {
            List<UserViewModel> users = repo.GetAllUsers();

            if(users == null)
            {
                return BadRequest();
            }
            else if(users.Count == 0)
            {
                return NotFound();
            }
            else
            {
                return Ok(users);
            }
        }

        /// <summary>
        /// List all partipating rides of user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("myrides")]
        [ResponseType(typeof(List<RideViewModel>))]
        public IHttpActionResult GetMyRides([FromUri] string username)
        {
            List<RideViewModel> rides = repo.GetMyRides(username);

            if (rides?.Count == 0)
            {
                return NotFound();
            }
            else if (rides?.Count > 0)
            {
                return Ok(rides);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Edit user
        /// </summary>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPut]
        [Route("")]
        public IHttpActionResult EditUser(EditUserViewModel editUserViewModel)
        {
            ApplicationUser user = (ApplicationUser)User;
            bool result = false;

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            UserViewModel userViewModel = new UserViewModel();
            userViewModel.UserID = user.Id;
            userViewModel.Username = user.Username;
            userViewModel.DateOfBirth = editUserViewModel.DateOfBirth;
            userViewModel.Description = editUserViewModel.Description;
            userViewModel.DpUrl = editUserViewModel.DpUrl;
            userViewModel.Region = editUserViewModel.Region;
            userViewModel.Ville = editUserViewModel.Ville;

            result = repo.EditUser(userViewModel);

            if(result)
            {
                return Ok("User successfully edited");
            }
            else
            {
                return BadRequest("Error while editing user.");
            }
        }

        /// <summary>
        /// Edit user password
        /// </summary>
        /// <param name="pwd"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpPut]
        [Route("password")]
        public IHttpActionResult EditUserPassword([FromUri] string pwd)
        {
            ApplicationUser user = (ApplicationUser)User;
            bool result = false;

            result = repo.EditUserPwd(user.Username, pwd);

            if (result)
            {
                return Ok("User password successfully edited");
            }
            else
            {
                return BadRequest("Error while editing user password.");
            }
        }

        /// <summary>
        /// Get User by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult GetUserById([FromUri] string username)
        {
            UserViewModel user = repo.GetUserByName(username);

            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get User by Authorization token
        /// </summary>
        /// <param name="auth_token"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpGet]
        [Route("bytoken")]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult GetUserByAuthToken()
        {
            ApplicationUser user = (ApplicationUser)User;

            UserViewModel userViewModel = repo.GetUserByTokenIfLastTokenIsValid(user.Token);

            if (userViewModel != null)
            {
                return Ok(userViewModel);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
