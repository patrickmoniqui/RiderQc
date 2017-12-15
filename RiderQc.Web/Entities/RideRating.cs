using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RiderQc.Web.Entities
{
    [Table("RideRating")]
    public partial class RideRating
    {
        public int RideRatingId { get; set; }

        public int RaterId { get; set; }

        public int RatedRideId { get; set; }

        public int Rate { get; set; }

        [StringLength(200)]
        public string RatingMessage { get; set; }

        public virtual User User { get; set; }

        public virtual Ride Ride { get; set; }
    }
}