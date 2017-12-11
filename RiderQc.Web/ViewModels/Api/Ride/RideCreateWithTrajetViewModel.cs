
using Newtonsoft.Json;
using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.Trajet;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Ride
{
    public class RideCreateWithTrajetViewModel
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public int CreatorId { get; set; }
        [Required]
        public TrajetCreateViewModel Trajet { get; set; }
        public int LevelId { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateFin { get; set; }
    }
}