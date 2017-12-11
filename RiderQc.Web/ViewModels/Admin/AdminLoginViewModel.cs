using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Admin
{
    public class AdminLoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}