namespace RiderQc.Web.DAL.Entity
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserLevel")]
    public partial class UserLevel
    {
        [Key]
        public int LevelPreferenceId { get; set; }

        public int UserId { get; set; }

        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public virtual User User { get; set; }
    }
}
