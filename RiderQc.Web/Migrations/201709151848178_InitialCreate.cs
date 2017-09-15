namespace RiderQc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comment",
                c => new
                    {
                        CommentId = c.Int(nullable: false, identity: true),
                        ParentId = c.Int(),
                        SenderId = c.Int(nullable: false),
                        RideId = c.Int(),
                        TrajetId = c.Int(),
                        CommentText = c.String(maxLength: 1000),
                        Blocked = c.Boolean(nullable: false),
                        Vote = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Comment", t => t.ParentId)
                .ForeignKey("dbo.Ride", t => t.RideId)
                .ForeignKey("dbo.User", t => t.SenderId)
                .ForeignKey("dbo.Trajet", t => t.TrajetId)
                .Index(t => t.ParentId)
                .Index(t => t.SenderId)
                .Index(t => t.RideId)
                .Index(t => t.TrajetId);
            
            CreateTable(
                "dbo.Ride",
                c => new
                    {
                        RideId = c.Int(nullable: false, identity: true),
                        TrajetId = c.Int(),
                        CreatorId = c.Int(nullable: false),
                        Depart = c.DateTime(nullable: false),
                        Arrive = c.DateTime(nullable: false),
                        description = c.String(maxLength: 200),
                        levelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.RideId)
                .ForeignKey("dbo.Level", t => t.levelId)
                .ForeignKey("dbo.User", t => t.CreatorId)
                .ForeignKey("dbo.Trajet", t => t.TrajetId)
                .Index(t => t.TrajetId)
                .Index(t => t.CreatorId)
                .Index(t => t.levelId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        LevelId = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.LevelId);
            
            CreateTable(
                "dbo.UserLevel",
                c => new
                    {
                        LevelPreferenceId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LevelPreferenceId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Level", t => t.LevelPreferenceId)
                .Index(t => t.LevelPreferenceId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        Region = c.String(maxLength: 50),
                        Ville = c.String(maxLength: 50),
                        Age = c.Int(),
                        Description = c.String(maxLength: 500),
                        DpUrl = c.String(maxLength: 50),
                        rate = c.Decimal(precision: 18, scale: 0),
                    })
                .PrimaryKey(t => t.UserID);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        MessageText = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .ForeignKey("dbo.User", t => t.SenderId)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Moto",
                c => new
                    {
                        MotoId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Brand = c.String(nullable: false, maxLength: 25),
                        Model = c.String(maxLength: 25),
                        Year = c.Int(),
                        Type = c.Int(),
                    })
                .PrimaryKey(t => t.MotoId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Trajet",
                c => new
                    {
                        TrajetId = c.Int(nullable: false, identity: true),
                        CreatorId = c.Int(nullable: false),
                        GoogleCo = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.TrajetId)
                .ForeignKey("dbo.User", t => t.CreatorId)
                .Index(t => t.CreatorId);
            
            CreateTable(
                "dbo.UserRating",
                c => new
                    {
                        UserRatingId = c.Int(nullable: false, identity: true),
                        RaterId = c.Int(nullable: false),
                        RatedId = c.Int(nullable: false),
                        Rate = c.Int(nullable: false),
                        RatingMessage = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.UserRatingId)
                .ForeignKey("dbo.User", t => t.RatedId)
                .ForeignKey("dbo.User", t => t.RaterId)
                .Index(t => t.RaterId)
                .Index(t => t.RatedId);
            
            CreateTable(
                "dbo.UserRide",
                c => new
                    {
                        UserRideId = c.Int(nullable: false, identity: true),
                        RideId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRideId)
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.Ride", t => t.RideId)
                .Index(t => t.RideId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRide", "RideId", "dbo.Ride");
            DropForeignKey("dbo.UserLevel", "LevelPreferenceId", "dbo.Level");
            DropForeignKey("dbo.UserRide", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRating", "RaterId", "dbo.User");
            DropForeignKey("dbo.UserRating", "RatedId", "dbo.User");
            DropForeignKey("dbo.UserLevel", "UserId", "dbo.User");
            DropForeignKey("dbo.Trajet", "CreatorId", "dbo.User");
            DropForeignKey("dbo.Ride", "TrajetId", "dbo.Trajet");
            DropForeignKey("dbo.Comment", "TrajetId", "dbo.Trajet");
            DropForeignKey("dbo.Ride", "CreatorId", "dbo.User");
            DropForeignKey("dbo.Moto", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "SenderId", "dbo.User");
            DropForeignKey("dbo.Message", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.Comment", "SenderId", "dbo.User");
            DropForeignKey("dbo.Ride", "levelId", "dbo.Level");
            DropForeignKey("dbo.Comment", "RideId", "dbo.Ride");
            DropForeignKey("dbo.Comment", "ParentId", "dbo.Comment");
            DropIndex("dbo.UserRide", new[] { "UserId" });
            DropIndex("dbo.UserRide", new[] { "RideId" });
            DropIndex("dbo.UserRating", new[] { "RatedId" });
            DropIndex("dbo.UserRating", new[] { "RaterId" });
            DropIndex("dbo.Trajet", new[] { "CreatorId" });
            DropIndex("dbo.Moto", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "ReceiverId" });
            DropIndex("dbo.Message", new[] { "SenderId" });
            DropIndex("dbo.UserLevel", new[] { "UserId" });
            DropIndex("dbo.UserLevel", new[] { "LevelPreferenceId" });
            DropIndex("dbo.Ride", new[] { "levelId" });
            DropIndex("dbo.Ride", new[] { "CreatorId" });
            DropIndex("dbo.Ride", new[] { "TrajetId" });
            DropIndex("dbo.Comment", new[] { "TrajetId" });
            DropIndex("dbo.Comment", new[] { "RideId" });
            DropIndex("dbo.Comment", new[] { "SenderId" });
            DropIndex("dbo.Comment", new[] { "ParentId" });
            DropTable("dbo.UserRide");
            DropTable("dbo.UserRating");
            DropTable("dbo.Trajet");
            DropTable("dbo.Moto");
            DropTable("dbo.Message");
            DropTable("dbo.User");
            DropTable("dbo.UserLevel");
            DropTable("dbo.Level");
            DropTable("dbo.Ride");
            DropTable("dbo.Comment");
        }
    }
}
