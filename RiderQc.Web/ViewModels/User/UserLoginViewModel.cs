using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.User
{
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required, MinLength(8), MaxLength(25)]
        public string Password { get; set; }
    }
}