namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ride")]
    public partial class Ride
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Ride()
        {
            Comments = new HashSet<Comment>();
            UserRides = new HashSet<UserRide>();
        }

        public int RideId { get; set; }

        public int? TrajetId { get; set; }

        public int CreatorId { get; set; }

        public DateTime DateDepart { get; set; }

        public DateTime DateFin { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public int LevelId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        public virtual Level Level { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserRide> UserRides { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Participants { get; set; }

        public virtual Trajet Trajet { get; set; }

        public virtual User User { get; set; }
    }
}
