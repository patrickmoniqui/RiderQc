namespace RiderQc.Web.Entities
{
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("UserRole")]
    public partial class UserRole
    {
        public int UserRoleId { get; set; }

        public int RoleId { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual Role Role { get; set; }
    }
}