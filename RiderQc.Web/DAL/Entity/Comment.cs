namespace RiderQc.Web.DAL.Entity
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Comment")]
    public partial class Comment
    {
        public Comment()
        {
            Comment1 = new HashSet<Comment>();
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

        public virtual ICollection<Comment> Comment1 { get; set; }

        public virtual Comment Comment2 { get; set; }

        public virtual Ride Ride { get; set; }

        public virtual User User { get; set; }

        public virtual Trajet Trajet { get; set; }
    }
}
