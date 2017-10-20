using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace RiderQc.Web.App_Start
{
    public class AuthTokenAuthorization : AuthorizationFilterAttribute
    {
        private readonly IUserRepository repo;
        private UserViewModel user;

        public AuthTokenAuthorization()
        {
            user = null;
            repo = DependencyResolver.Current.GetService<IUserRepository>();
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            string token = "";

            IEnumerable<string> headerValues = null;
            actionContext.Request.Headers.TryGetValues("Authorization", out headerValues);
            
            if(headerValues != null && headerValues.Count() >= 1)
            {
                var authHeader = headerValues.FirstOrDefault();
                token = authHeader.Replace("Bearer", "").Trim();
            }
            else
            {
                UnAuthorized(actionContext);
                return;
            }
            
            // User is authorized
            if (OnAuthorizeUser(token, actionContext))
            {
                ApplicationUser AppUser = new ApplicationUser();
                AppUser.Id = user.UserID;
                AppUser.Username = user.Username;

                IPrincipal principal = AppUser;
                actionContext.RequestContext.Principal = principal;
            }
            else
            {

                UnAuthorized(actionContext);
                return;
            } 
        }

        /// <summary>
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected virtual bool OnAuthorizeUser(string authToken, HttpActionContext actionContext)
        {
            if (string.IsNullOrEmpty(authToken))
            {
                return false;
            }
            else
            {
               user = repo.GetUserByTokenIsLastTokenIsValid(authToken);

                return (user != null) ? true : false;
            }
        }

        private void UnAuthorized(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Unauthorized");
            return;
        }
    }
}