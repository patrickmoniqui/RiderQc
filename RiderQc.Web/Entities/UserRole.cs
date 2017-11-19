namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserRole")]
    public partial class UserRole
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserRoleId { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
