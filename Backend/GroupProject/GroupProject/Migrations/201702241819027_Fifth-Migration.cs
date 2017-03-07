namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FifthMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rooms", "RoomNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "BirthDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Rooms", "RoomNumber");
        }
    }
}
