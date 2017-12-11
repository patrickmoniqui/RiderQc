using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RiderQc.Web.ViewModels.User
{
    public class AuthentificationTokenViewModel
    {
        public string Token { get; set; }
        public System.DateTime IssueDate { get; set; }
        public System.DateTime ExpirationDate { get; set; }
    }
}