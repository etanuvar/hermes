namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WrongRefFIx : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CalendarComponent", "UserGroupId", "dbo.UserGroup");
            DropIndex("dbo.CalendarComponent", new[] { "UserGroupId" });
            AddColumn("dbo.CalendarComponent", "GroupId", c => c.Int());
            CreateIndex("dbo.CalendarComponent", "GroupId");
            AddForeignKey("dbo.CalendarComponent", "GroupId", "dbo.Group", "Id");
            DropColumn("dbo.CalendarComponent", "UserGroupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CalendarComponent", "UserGroupId", c => c.Int());
            DropForeignKey("dbo.CalendarComponent", "GroupId", "dbo.Group");
            DropIndex("dbo.CalendarComponent", new[] { "GroupId" });
            DropColumn("dbo.CalendarComponent", "GroupId");
            CreateIndex("dbo.CalendarComponent", "UserGroupId");
            AddForeignKey("dbo.CalendarComponent", "UserGroupId", "dbo.UserGroup", "Id");
        }
    }
}
