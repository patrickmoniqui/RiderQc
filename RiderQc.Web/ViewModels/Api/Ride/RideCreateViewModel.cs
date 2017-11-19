
using Newtonsoft.Json;
using RiderQc.Web.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Ride
{
    public class RideCreateViewModel
    {
        [JsonIgnore]
        public int RideId { get; set; }

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