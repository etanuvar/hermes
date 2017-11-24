namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnContactType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarComponent", "SelectedContactType", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalendarComponent", "SelectedContactType");
        }
    }
}
