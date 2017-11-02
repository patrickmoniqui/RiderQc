using RiderQc.Web.ViewModels.User;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Trajet
{
    public class TrajetViewModel
    {
        public int TrajetId { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public UserSimpleViewModel Creator { get; set; }
        public List<string> GpsPoints { get; set; }
    }
}