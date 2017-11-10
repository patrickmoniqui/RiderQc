using System;
using System.Collections.Generic;

namespace RiderQc.Web.ViewModels.Admin
{
    public class UserAdminViewModel
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Region { get; set; }
        public string Ville { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Description { get; set; }
        public string DpUrl { get; set; }
        public ICollection<ViewModels.Moto.MotoViewModel> Motos { get; set; }
        public ICollection<ViewModels.Ride.RideViewModel> Rides { get; set; }
    }
}