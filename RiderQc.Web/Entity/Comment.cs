namespace RiderQc.Web.DAL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Comment")]
    public partial class Comment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Comment()
        {
            Comment1 = new HashSet<Comment>();
            Comment11 = new HashSet<Comment>();
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
        public virtual ICollection<Comment> Comment1 { get; set; }

        public virtual Comment Comment2 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comment11 { get; set; }

        public virtual Comment Comment3 { get; set; }

        public virtual Ride Ride { get; set; }

        public virtual Ride Ride1 { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }

        public virtual Trajet Trajet { get; set; }

        public virtual Trajet Trajet1 { get; set; }
    }
}
