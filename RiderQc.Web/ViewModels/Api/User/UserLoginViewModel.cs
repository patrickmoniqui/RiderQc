using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.User
{
    public class UserLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required, MinLength(6), MaxLength(100)]
        public string Password { get; set; }
    }
}