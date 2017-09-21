using System;

namespace RiderQc.Web.ViewModels.Ride
{
    public class RideViewModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public int CreatorId { get; set; }
        public int? TrajetId { get; set; }
        public int LevelId { get; set; }
        public DateTime DateDepart { get; set; }
        public DateTime DateFin { get; set; }
    }
}