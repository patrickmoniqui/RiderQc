namespace RiderQc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserPartipants : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RideUsers",
                c => new
                    {
                        Ride_RideId = c.Int(nullable: false),
                        User_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Ride_RideId, t.User_UserID })
                .ForeignKey("dbo.Ride", t => t.Ride_RideId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.User_UserID, cascadeDelete: true)
                .Index(t => t.Ride_RideId)
                .Index(t => t.User_UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RideUsers", "User_UserID", "dbo.User");
            DropForeignKey("dbo.RideUsers", "Ride_RideId", "dbo.Ride");
            DropIndex("dbo.RideUsers", new[] { "User_UserID" });
            DropIndex("dbo.RideUsers", new[] { "Ride_RideId" });
            DropTable("dbo.RideUsers");
        }
    }
}
