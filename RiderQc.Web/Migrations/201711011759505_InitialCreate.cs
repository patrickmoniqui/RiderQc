namespace RiderQc.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authentification",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        Token = c.String(nullable: false, unicode: false),
                        IssueDate = c.DateTime(nullable: false),
                        ExpirationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Region = c.String(maxLength: 50),
                        Ville = c.String(maxLength: 50),
                        DateOfBirth = c.DateTime(),
                        Description = c.String(maxLength: 500),
                        DpUrl = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
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
                        TimeStamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CommentId)
                .ForeignKey("dbo.Comment", t => t.ParentId)
                .ForeignKey("dbo.Ride", t => t.RideId)
                .ForeignKey("dbo.Trajet", t => t.TrajetId)
                .ForeignKey("dbo.User", t => t.SenderId)
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
                        DateDepart = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 200),
                        LevelId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.RideId)
                .ForeignKey("dbo.Level", t => t.LevelId)
                .ForeignKey("dbo.Trajet", t => t.TrajetId)
                .ForeignKey("dbo.User", t => t.CreatorId)
                .Index(t => t.TrajetId)
                .Index(t => t.CreatorId)
                .Index(t => t.LevelId);
            
            CreateTable(
                "dbo.Level",
                c => new
                    {
                        LevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.LevelId);
            
            CreateTable(
                "dbo.UserLevel",
                c => new
                    {
                        UserLevelId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        LevelId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserLevelId)
                .ForeignKey("dbo.Level", t => t.LevelId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.LevelId);
            
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
                "dbo.UserRide",
                c => new
                    {
                        UserRideId = c.Int(nullable: false, identity: true),
                        RideId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserRideId)
                .ForeignKey("dbo.Ride", t => t.RideId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.RideId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Message",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        SenderId = c.Int(nullable: false),
                        ReceiverId = c.Int(nullable: false),
                        MessageText = c.String(maxLength: 1000),
                        TimeStamp = c.DateTime(nullable: false),
                        Read = c.DateTime(),
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
                "dbo.sysdiagrams",
                c => new
                    {
                        diagram_id = c.Int(nullable: false, identity: true),
                        name = c.String(nullable: false, maxLength: 128),
                        principal_id = c.Int(nullable: false),
                        version = c.Int(),
                        definition = c.Binary(),
                    })
                .PrimaryKey(t => t.diagram_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRide", "UserId", "dbo.User");
            DropForeignKey("dbo.UserRating", "RaterId", "dbo.User");
            DropForeignKey("dbo.UserRating", "RatedId", "dbo.User");
            DropForeignKey("dbo.UserLevel", "UserId", "dbo.User");
            DropForeignKey("dbo.Trajet", "CreatorId", "dbo.User");
            DropForeignKey("dbo.Ride", "CreatorId", "dbo.User");
            DropForeignKey("dbo.Moto", "UserId", "dbo.User");
            DropForeignKey("dbo.Message", "SenderId", "dbo.User");
            DropForeignKey("dbo.Message", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.Comment", "SenderId", "dbo.User");
            DropForeignKey("dbo.UserRide", "RideId", "dbo.Ride");
            DropForeignKey("dbo.Ride", "TrajetId", "dbo.Trajet");
            DropForeignKey("dbo.Comment", "TrajetId", "dbo.Trajet");
            DropForeignKey("dbo.UserLevel", "LevelId", "dbo.Level");
            DropForeignKey("dbo.Ride", "LevelId", "dbo.Level");
            DropForeignKey("dbo.Comment", "RideId", "dbo.Ride");
            DropForeignKey("dbo.Comment", "ParentId", "dbo.Comment");
            DropForeignKey("dbo.Authentification", "UserId", "dbo.User");
            DropIndex("dbo.UserRating", new[] { "RatedId" });
            DropIndex("dbo.UserRating", new[] { "RaterId" });
            DropIndex("dbo.Moto", new[] { "UserId" });
            DropIndex("dbo.Message", new[] { "ReceiverId" });
            DropIndex("dbo.Message", new[] { "SenderId" });
            DropIndex("dbo.UserRide", new[] { "UserId" });
            DropIndex("dbo.UserRide", new[] { "RideId" });
            DropIndex("dbo.Trajet", new[] { "CreatorId" });
            DropIndex("dbo.UserLevel", new[] { "LevelId" });
            DropIndex("dbo.UserLevel", new[] { "UserId" });
            DropIndex("dbo.Ride", new[] { "LevelId" });
            DropIndex("dbo.Ride", new[] { "CreatorId" });
            DropIndex("dbo.Ride", new[] { "TrajetId" });
            DropIndex("dbo.Comment", new[] { "TrajetId" });
            DropIndex("dbo.Comment", new[] { "RideId" });
            DropIndex("dbo.Comment", new[] { "SenderId" });
            DropIndex("dbo.Comment", new[] { "ParentId" });
            DropIndex("dbo.Authentification", new[] { "UserId" });
            DropTable("dbo.sysdiagrams");
            DropTable("dbo.UserRating");
            DropTable("dbo.Moto");
            DropTable("dbo.Message");
            DropTable("dbo.UserRide");
            DropTable("dbo.Trajet");
            DropTable("dbo.UserLevel");
            DropTable("dbo.Level");
            DropTable("dbo.Ride");
            DropTable("dbo.Comment");
            DropTable("dbo.User");
            DropTable("dbo.Authentification");
        }
    }
}
