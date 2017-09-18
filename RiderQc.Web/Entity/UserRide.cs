namespace RiderQc.Web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRide")]
    public partial class UserRide
    {
        public int RideId { get; set; }

        public int UserId { get; set; }

        public int UserRideId { get; set; }

        public virtual Ride Ride { get; set; }

        public virtual User User { get; set; }
    }
}
