namespace RiderQc.Web.DAL.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Trajet")]
    public partial class Trajet
    {
        public Trajet()
        {
            Comments = new HashSet<Comment>();
            Rides = new HashSet<Ride>();
        }

        public int TrajetId { get; set; }

        public int CreatorId { get; set; }

        [StringLength(500)]
        public string GoogleCo { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }

        public virtual User User { get; set; }
    }
}
