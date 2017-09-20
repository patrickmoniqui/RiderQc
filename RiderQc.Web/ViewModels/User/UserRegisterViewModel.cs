using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.ViewModels.User
{
    public class UserRegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string DpUrl { get; set; }
    }
}