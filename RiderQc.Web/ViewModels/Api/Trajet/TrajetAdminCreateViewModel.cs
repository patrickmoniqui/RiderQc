using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Api.Trajet
{
    public class TrajetAdminCreateViewModel
    {
        [Required]
        public int CreatorId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }
    }
}