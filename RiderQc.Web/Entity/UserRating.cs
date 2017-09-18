namespace RiderQc.Web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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

        public virtual User User2 { get; set; }

        public virtual User User3 { get; set; }
    }
}
