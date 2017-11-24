namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixRefferences : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Attachment", name: "TaskItem_Id", newName: "TaskItemId");
            RenameColumn(table: "dbo.Attachment", name: "CalendarComponentId", newName: "ContactId");
            RenameIndex(table: "dbo.Attachment", name: "IX_TaskItem_Id", newName: "IX_TaskItemId");
            RenameIndex(table: "dbo.Attachment", name: "IX_CalendarComponentId", newName: "IX_ContactId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Attachment", name: "IX_ContactId", newName: "IX_CalendarComponentId");
            RenameIndex(table: "dbo.Attachment", name: "IX_TaskItemId", newName: "IX_TaskItem_Id");
            RenameColumn(table: "dbo.Attachment", name: "ContactId", newName: "CalendarComponentId");
            RenameColumn(table: "dbo.Attachment", name: "TaskItemId", newName: "TaskItem_Id");
        }
    }
}
