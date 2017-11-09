namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Comment")]
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            ChildComments = new HashSet<Comment>();
        }

        public int CommentId { get; set; }

        public int? ParentId { get; set; }

        public int SenderId { get; set; }

        public int? RideId { get; set; }

        public int? TrajetId { get; set; }

        [StringLength(1000)]
        public string CommentText { get; set; }

        public bool Blocked { get; set; }

        public int Vote { get; set; }

        public DateTime TimeStamp { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> ChildComments { get; set; }

        public virtual Comment ParentComment { get; set; }

        public virtual Ride Ride { get; set; }

        public virtual User User { get; set; }

        public virtual Trajet Trajet { get; set; }
    }
}
