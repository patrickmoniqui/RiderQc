
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
        public List<RiderQc.Web.ViewModels.Comment.CommentViewModel> Comments { get; set; }
        public RiderQc.Web.ViewModels.Level.LevelViewModel Level { get; set; }
        public RiderQc.Web.ViewModels.Trajet.TrajetViewModel Trajet { get; set; }
        public RiderQc.Web.ViewModels.User.UserViewModel User { get; set; }

    }
}