namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeadlineDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarComponent", "DeadlineDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarComponent", "DeadlineDate");
        }
    }
}
