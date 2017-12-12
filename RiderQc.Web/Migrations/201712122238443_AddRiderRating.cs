namespace RiderQc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRiderRating : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RideRating",
                c => new
                    {
                        RideRatingId = c.Int(nullable: false, identity: true),
                        RaterId = c.Int(nullable: false),
                        RatedRideId = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        RatingMessage = c.String(maxLength: 200, unicode: false),
                    })
                .PrimaryKey(t => t.RideRatingId)
                .ForeignKey("dbo.Ride", t => t.RatedRideId)
                .ForeignKey("dbo.User", t => t.RaterId)
                .Index(t => t.RatedRideId)
                .Index(t => t.RaterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RideRating", "User_UserID", "dbo.User");
            DropForeignKey("dbo.RideRating", "Ride_RideId", "dbo.Ride");
            DropIndex("dbo.RideRating", new[] { "User_UserID" });
            DropIndex("dbo.RideRating", new[] { "Ride_RideId" });
            DropTable("dbo.RideRating");
        }
    }
}
