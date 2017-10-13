using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace RiderQc.Web.Models
{
    public class ApplicationUser : System.Security.Principal.IPrincipal
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public IIdentity Identity { get; set; }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}