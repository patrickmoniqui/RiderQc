
using Newtonsoft.Json;
using RiderQc.Web.Entities;
using System;
using System.Collections.Generic;
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
        public ICollection<Comment> Comments { get; set; }
        public RiderQc.Web.Entities.Level Level { get; set; }
        public ICollection<UserRide> UserRides { get; set; }
        public RiderQc.Web.Entities.Trajet Trajet { get; set; }
        public RiderQc.Web.Entities.User User { get; set; }

    }
}