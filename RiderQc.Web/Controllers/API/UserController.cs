using RiderQc.Web.Entities;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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

            string username = Base64Decode(userViewModel.Username);

            if (repo.CheckUserExistence(username))
                return BadRequest("That username is already taken.");

            userViewModel.Username = username;

            string password = Base64Decode(userViewModel.Password);

            userViewModel.Password = HashToSHA256(password);

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
        #region Methods for Encryption/Decryption
        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static String HashToSHA256(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (byte b in result)
                    Sb.Append(b.ToString("x2"));
            }
            return Sb.ToString();
        }
        #endregion
    }
}
