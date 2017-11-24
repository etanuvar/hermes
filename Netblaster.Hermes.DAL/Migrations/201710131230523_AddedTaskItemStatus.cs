namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTaskItemStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarComponent", "ItemStatus", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarComponent", "ItemStatus");
        }
    }
}
