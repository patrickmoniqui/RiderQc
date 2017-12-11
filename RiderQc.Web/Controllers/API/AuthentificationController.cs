using RiderQc.Web.App_Start;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("")]
    public class AuthentificationController : ApiController
    {
        private readonly IUserRepository repo;

        public AuthentificationController(IUserRepository _repo)
        {
            repo = _repo;
            //salut
        }

        /// <summary>
        /// Renew token for user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("token/renew")]
        [ResponseType(typeof(AuthentificationTokenViewModel))]
        public IHttpActionResult RenewToken()
        {
            UserLoginViewModel user = null;
            bool result = false;
            string userCreds = "";

            IEnumerable<string> AuthHeader = null;
            if (Request.Headers.TryGetValues("Authorization", out AuthHeader))
            {
                userCreds = Request.Headers.GetValues("Authorization").FirstOrDefault();
            }

            if (userCreds == "")
            {
                return BadRequest();
            }

            string base64 = userCreds.Replace("Basic", "");

            string[] UserPwd = new string[2];
            try
            {
                UserPwd = Encoding.UTF8.GetString(Convert.FromBase64String(base64.Trim())).Split(':');
            }
            catch (Exception ex)
            {
                //Cant encode base64
            }

            if (UserPwd != null && UserPwd[0] != null && UserPwd[1] != null)
            {
                user = new UserLoginViewModel();
                user.Username = UserPwd[0];
                user.Password = UserPwd[1];

                result = repo.CredentialsAreValid(user.Username, user.Password);
            }

            // User is valid.
            if (result)
            {
                AuthentificationTokenViewModel token = repo.GenerateTokenForUser(user.Username);

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }

        /// <summary>
        /// Login
        /// </summary>
        /// <returns>Authentification Token</returns>
        [HttpGet]
        [Route("login")]
        [ResponseType(typeof(AuthentificationTokenViewModel))]
        public IHttpActionResult Login([FromUri] bool rememberme = true, [FromUri] int expires = 30)
        {
            UserLoginViewModel user = null;
            bool result = false;
            string userCreds = "";

            IEnumerable<string> AuthHeader = null;
            if(Request.Headers.TryGetValues("Authorization", out AuthHeader))
            {
                userCreds = Request.Headers.GetValues("Authorization").FirstOrDefault();
            }
            
            if (userCreds == "")
            {
                return BadRequest();
            }

            string base64 = userCreds.Replace("Basic", "");
            
            string[] UserPwd = new string[2];
            try
            {
                UserPwd = Encoding.UTF8.GetString(Convert.FromBase64String(base64.Trim())).Split(':');
            }
            catch (Exception ex)
            {
                //Cant encode base64
            }

            if (UserPwd != null && UserPwd[0] != null && UserPwd[1] != null)
            {
                user = new UserLoginViewModel();
                user.Username = UserPwd[0];
                user.Password = UserPwd[1];

                result = repo.CredentialsAreValid(user.Username, user.Password);
            }
            
            // User is valid.
            if(result)
            {
                if(!rememberme)
                {
                    return Ok();
                }

                AuthentificationTokenViewModel token = repo.GetLastValidTokenByUsername(user.Username);
                
                // We need to generate a token if none found.
                if(token == null)
                {
                    token = repo.GenerateTokenForUser(user.Username, expires);
                }

                return Ok(token);
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}