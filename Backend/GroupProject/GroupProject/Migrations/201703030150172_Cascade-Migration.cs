namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CascadeMigration : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "ReceiverUserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "SenderUserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Rooms", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "RoomId", "dbo.Rooms");
            AddForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations", "ConversationId", cascadeDelete: true);
            AddForeignKey("dbo.Conversations", "ReceiverUserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Conversations", "SenderUserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Favorites", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Rooms", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.Favorites", "RoomId", "dbo.Rooms", "RoomId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favorites", "RoomId", "dbo.Rooms");
            DropForeignKey("dbo.Rooms", "UserId", "dbo.Users");
            DropForeignKey("dbo.Favorites", "UserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "SenderUserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "ReceiverUserId", "dbo.Users");
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            AddForeignKey("dbo.Favorites", "RoomId", "dbo.Rooms", "RoomId");
            AddForeignKey("dbo.Rooms", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Favorites", "UserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Conversations", "SenderUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Conversations", "ReceiverUserId", "dbo.Users", "UserId");
            AddForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations", "ConversationId");
        }
    }
}
