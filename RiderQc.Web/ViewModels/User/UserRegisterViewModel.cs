using System;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.User
{
    public class UserRegisterViewModel
    {
        [Required, MinLength(4), MaxLength(30)]
        public string Username { get; set; }
        [Required, MinLength(8), MaxLength(25)]
        public string Password { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string DpUrl { get; set; }
    }
}