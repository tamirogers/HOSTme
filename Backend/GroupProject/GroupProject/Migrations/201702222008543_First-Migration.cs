namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.Int(nullable: false, identity: true),
                        SenderUserId = c.Int(nullable: false),
                        ReceiverUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ConversationId)
                .ForeignKey("dbo.Users", t => t.ReceiverUserId)
                .ForeignKey("dbo.Users", t => t.SenderUserId)
                .Index(t => t.SenderUserId)
                .Index(t => t.ReceiverUserId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        ConversationId = c.Int(nullable: false),
                        Subject = c.String(),
                        Body = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Conversations", t => t.ConversationId)
                .Index(t => t.ConversationId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        UserName = c.String(),
                        Zip = c.String(),
                        ContactPhone = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Password = c.String(),
                        ProfilePic = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Favorites",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        RoomId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoomId })
                .ForeignKey("dbo.Rooms", t => t.RoomId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RoomId);
            
            CreateTable(
                "dbo.Rooms",
                c => new
                    {
                        RoomId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoomName = c.String(),
                        Description = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Zip = c.String(),
                        Price = c.Int(nullable: false),
                        PictureUrl = c.String(),
                        Private = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.RoomId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Conversations", "SenderUserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "ReceiverUserId", "dbo.Users");
            DropForeignKey("dbo.Rooms", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropIndex("dbo.Rooms", new[] { "UserId" });
            DropIndex("dbo.Favorites", new[] { "RoomId" });
            DropIndex("dbo.Favorites", new[] { "UserId" });
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Conversations", new[] { "ReceiverUserId" });
            DropIndex("dbo.Conversations", new[] { "SenderUserId" });
            DropTable("dbo.Rooms");
            DropTable("dbo.Favorites");
            DropTable("dbo.Users");
            DropTable("dbo.Messages");
            DropTable("dbo.Conversations");
        }
    }
}
