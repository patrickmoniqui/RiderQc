using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiderQc.Web.DAL
{
    [Table("Ride")]
    public partial class Ride
    {
        public Ride()
        {
            Comments = new List<Comment>();
            Comments1 = new List<Comment>();
            UserRides = new List<UserRide>();
        }

        public int RideId { get; set; }

        public int? TrajetId { get; set; }

        public int CreatorId { get; set; }

        public DateTime Depart { get; set; }

        public DateTime Arrive { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int LevelId { get; set; }

        public virtual List<Comment> Comments { get; set; }

        public virtual List<Comment> Comments1 { get; set; }

        public virtual Level Level { get; set; }

        public virtual List<UserRide> UserRides { get; set; }

        public virtual Trajet Trajet { get; set; }

        public virtual User User { get; set; }
    }
}
