namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Message")]
    public partial class Message
    {
        public int MessageId { get; set; }

        public int SenderId { get; set; }

        public int ReceiverId { get; set; }

        [StringLength(1000)]
        public string MessageText { get; set; }

        public DateTime TimeStamp { get; set; }

        public DateTime? Read { get; set; }

        public virtual User Receiver { get; set; }

        public virtual User Sender { get; set; }
    }
}
