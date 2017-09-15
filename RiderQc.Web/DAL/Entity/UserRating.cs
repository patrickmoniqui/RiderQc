namespace RiderQc.Web.DAL.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRating")]
    public partial class UserRating
    {
        public int UserRatingId { get; set; }

        public int RaterId { get; set; }

        public int RatedId { get; set; }

        public int Rate { get; set; }

        [StringLength(200)]
        public string RatingMessage { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
