using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Ride
{
    public class RideViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public int CreatorId { get; set; }
        public int? TrajetId { get; set; }
        public int LevelId { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateFin { get; set; }
    }
}