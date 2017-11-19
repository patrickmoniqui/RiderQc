namespace RiderQc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRoleAndUserRole : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Role",
                c => new
                {
                    RoleId = c.Int(nullable: false, identity: true),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => new { t.RoleId })
                .Index(t => t.RoleId);

            CreateTable(
                "dbo.UserRole",
                c => new
                {
                    UserRoleId = c.Int(nullable: false, identity: true),
                    RoleId = c.Int(nullable: false),
                    UserID = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.UserRoleId, t.RoleId, t.UserID })
                .ForeignKey("dbo.Role", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserID, cascadeDelete: true)
                .Index(t => t.UserRoleId)
                .Index(t => t.RoleId)
                .Index(t => t.UserID);
        }
        
        public override void Down()
        {
        }
    }
}
