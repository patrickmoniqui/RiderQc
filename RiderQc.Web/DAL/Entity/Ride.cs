namespace RiderQc.Web.DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Ride")]
    public partial class Ride
    {
        public Ride()
        {
            Comments = new HashSet<Comment>();
            UserRides = new HashSet<UserRide>();
        }

        public int RideId { get; set; }

        public int? TrajetId { get; set; }

        public int CreatorId { get; set; }

        public DateTime Depart { get; set; }

        public DateTime Arrive { get; set; }

        [StringLength(200)]
        public string description { get; set; }

        public int levelId { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Level Level { get; set; }

        public virtual ICollection<UserRide> UserRides { get; set; }

        public virtual Trajet Trajet { get; set; }

        public virtual User User { get; set; }
    }
}
