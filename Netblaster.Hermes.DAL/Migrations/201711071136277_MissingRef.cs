namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissingRef : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Attachment", "TaskItem_Id", c => c.Int());
            CreateIndex("dbo.Attachment", "TaskItem_Id");
            AddForeignKey("dbo.Attachment", "TaskItem_Id", "dbo.CalendarComponent", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachment", "TaskItem_Id", "dbo.CalendarComponent");
            DropIndex("dbo.Attachment", new[] { "TaskItem_Id" });
            DropColumn("dbo.Attachment", "TaskItem_Id");
        }
    }
}
