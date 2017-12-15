namespace RiderQc.Web.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

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
