namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Authentification")]
    public partial class Authentification
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public DateTime IssueDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual User User { get; set; }
    }
}
