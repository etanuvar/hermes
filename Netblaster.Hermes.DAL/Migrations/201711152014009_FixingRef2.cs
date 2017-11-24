namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixingRef2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.CalendarComponent", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CalendarComponent", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CalendarComponent", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.CalendarComponent", new[] { "ApplicationUserId" });
            AlterColumn("dbo.CalendarComponent", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.CalendarComponent", "ApplicationUserId");
        }
    }
}
