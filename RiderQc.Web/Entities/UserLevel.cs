namespace RiderQc.Web.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("UserLevel")]
    public partial class UserLevel
    {
        public int UserLevelId { get; set; }

        public int UserId { get; set; }

        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public virtual User User { get; set; }
    }
}
