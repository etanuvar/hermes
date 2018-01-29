namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFields2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarComponent", "FinishedByDisplay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarComponent", "FinishedByDisplay");
        }
    }
}
