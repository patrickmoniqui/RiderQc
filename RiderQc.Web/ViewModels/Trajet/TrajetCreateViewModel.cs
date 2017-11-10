using Newtonsoft.Json;
using RiderQc.Web.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Trajet
{
    public class TrajetCreateViewModel
    {
        [JsonIgnore]
        public int TrajetId { get; set; }

        [JsonIgnore]
        public int CreatorId { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        [GpsCoordinateValidation]
        public List<string> GpsPoints { get; set; }
    }
}