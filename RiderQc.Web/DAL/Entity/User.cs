namespace RiderQc.Web.DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Messages = new HashSet<Message>();
            Messages1 = new HashSet<Message>();
            Motoes = new HashSet<Moto>();
            Rides = new HashSet<Ride>();
            Trajets = new HashSet<Trajet>();
            UserRatings = new HashSet<UserRating>();
            UserRatings1 = new HashSet<UserRating>();
            UserLevels = new HashSet<UserLevel>();
            UserRides = new HashSet<UserRide>();
        }

        public int UserID { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string Ville { get; set; }

        public int? Age { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [StringLength(50)]
        public string DpUrl { get; set; }

        public decimal? rate { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Message> Messages { get; set; }
        
        public virtual ICollection<Message> Messages1 { get; set; }

        public virtual ICollection<Moto> Motoes { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }

        public virtual ICollection<Trajet> Trajets { get; set; }

        public virtual ICollection<UserRating> UserRatings { get; set; }

        public virtual ICollection<UserRating> UserRatings1 { get; set; }

        public virtual ICollection<UserLevel> UserLevels { get; set; }

        public virtual ICollection<UserRide> UserRides { get; set; }
    }
}
