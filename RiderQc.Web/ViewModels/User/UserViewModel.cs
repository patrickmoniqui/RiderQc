using System;
using System.Collections.Generic;

namespace RiderQc.Web.ViewModels.User
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string DpUrl { get; set; }
        public ICollection<ViewModels.Moto.MotoViewModel> Motos { get; set; }
        public ICollection<ViewModels.Ride.RideViewModel> Rides { get; set; }
        public ICollection<ViewModels.Level.LevelViewModel> Levels { get; set; }
        public ICollection<ViewModels.Comment.CommentViewModel> Comments { get; set; }
        public ICollection<ViewModels.Message.MessageViewModel> Messages { get; set; }


    }
}