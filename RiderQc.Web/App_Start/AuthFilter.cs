using RiderQc.Web.Entities;
using RiderQc.Web.Helpers;
using RiderQc.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace RiderQc.Web.App_Start
{
    public class BasicAuthorization : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string username = "";
            string password = "";

            IEnumerable<string> headerValues = null;
            actionContext.Request.Headers.TryGetValues("Authentification", out headerValues);
            
            if(headerValues != null && headerValues.Count() >= 1)
            {
                var authHeader = headerValues.FirstOrDefault();
                authHeader = authHeader.Substring(6, authHeader.Length - 6);
                string base64 = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader));

                username = base64.Split(':')[0];
                password = base64.Split(':')[1];
            }
            else
            {
                UnAuthorized(actionContext);
                return;
            }
            
            // if user not authorize
            if (!OnAuthorizeUser(username, password, actionContext))
            {
                UnAuthorized(actionContext);
                return;
            }

            User identity = null;
            using (RiderQcContext ctx = new RiderQcContext())
            {
                string hashedPwd = EncryptionHelper.HashToSHA256(password);
                identity = ctx.Users.FirstOrDefault(x => x.Username == username && x.Password == hashedPwd);
            }

            ApplicationUser user = new ApplicationUser();
            user.Id = identity.UserID;
            user.Username = identity.Username;

            IPrincipal principal = user;
            actionContext.RequestContext.Principal = principal;
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected virtual bool OnAuthorizeUser(string username, string password, HttpActionContext actionContext)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                return false;
            }
            else
            {
                using (RiderQcContext ctx = new RiderQcContext())
                {
                    bool auth = false;
                    string hashedPwd = EncryptionHelper.HashToSHA256(password);

                    auth = ctx.Users.Any(x => x.Username == username && x.Password == hashedPwd);

                    return auth;
                }
            }
        }

        private void UnAuthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            return;
        }
    }
}