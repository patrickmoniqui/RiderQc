
using Newtonsoft.Json;
using RiderQc.Web.Entities;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RiderQc.Web.ViewModels.Ride
{
    public class RideViewModel
    {
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
        public List<RiderQc.Web.ViewModels.Comment.CommentViewModel> Comments { get; set; }
        public RiderQc.Web.ViewModels.Level.LevelViewModel Level { get; set; }
        public RiderQc.Web.ViewModels.Trajet.TrajetViewModel Trajet { get; set; }
        public UserSimpleViewModel Creator { get; set; }
        public List<string> Participants { get; set; }
        public double RideRating { get; set; }
    }
}