using RiderQc.Web.Entities;
using System;
using System.Collections.Generic;

namespace RiderQc.Web.ViewModels.User
{
    public class UserViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string DpUrl { get; set; }
        public virtual ICollection<Moto> Motoes { get; set; }
        public virtual ICollection<Entities.Ride> Rides { get; set; }
    }
}