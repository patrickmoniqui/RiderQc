namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Trajet")]
    public partial class Trajet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Trajet()
        {
            Comments = new HashSet<Comment>();
            Rides = new HashSet<Ride>();
        }

        public int TrajetId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int CreatorId { get; set; }

        [StringLength(500)]
        public string GoogleCo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Ride> Rides { get; set; }

        public virtual User User { get; set; }
    }
}
