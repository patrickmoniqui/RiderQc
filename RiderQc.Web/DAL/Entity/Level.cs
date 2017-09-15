namespace RiderQc.Web.DAL.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Level")]
    public partial class Level
    {
        public Level()
        {
            Rides = new HashSet<Ride>();
        }

        public int LevelId { get; set; }

        [Required]
        [StringLength(50)]
        public string name { get; set; }

        public virtual UserLevel UserLevel { get; set; }

        public virtual ICollection<Ride> Rides { get; set; }
    }
}
