namespace RiderQc.Web.DAL.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Message")]
    public partial class Message
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        [StringLength(1000)]
        public string MessageText { get; set; }

        public virtual User User { get; set; }

        public virtual User User1 { get; set; }
    }
}
