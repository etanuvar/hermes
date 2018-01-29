namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewFields : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalendarComponent", "KntId1", c => c.Int());
            AddColumn("dbo.CalendarComponent", "SelectedUserId", c => c.String(maxLength: 128));
            AddColumn("dbo.TaskDetail", "AttachmentId", c => c.Int());
            AddColumn("dbo.TaskSubItem", "FinishedById", c => c.String(maxLength: 128));
            AddColumn("dbo.TaskSubItem", "AttachmentId", c => c.Int());
            CreateIndex("dbo.CalendarComponent", "SelectedUserId");
            CreateIndex("dbo.TaskDetail", "AttachmentId");
            CreateIndex("dbo.TaskSubItem", "FinishedById");
            CreateIndex("dbo.TaskSubItem", "AttachmentId");
            AddForeignKey("dbo.CalendarComponent", "SelectedUserId", "dbo.User", "Id");
            AddForeignKey("dbo.TaskDetail", "AttachmentId", "dbo.Attachment", "Id");
            AddForeignKey("dbo.TaskSubItem", "AttachmentId", "dbo.Attachment", "Id");
            AddForeignKey("dbo.TaskSubItem", "FinishedById", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskSubItem", "FinishedById", "dbo.User");
            DropForeignKey("dbo.TaskSubItem", "AttachmentId", "dbo.Attachment");
            DropForeignKey("dbo.TaskDetail", "AttachmentId", "dbo.Attachment");
            DropForeignKey("dbo.CalendarComponent", "SelectedUserId", "dbo.User");
            DropIndex("dbo.TaskSubItem", new[] { "AttachmentId" });
            DropIndex("dbo.TaskSubItem", new[] { "FinishedById" });
            DropIndex("dbo.TaskDetail", new[] { "AttachmentId" });
            DropIndex("dbo.CalendarComponent", new[] { "SelectedUserId" });
            DropColumn("dbo.TaskSubItem", "AttachmentId");
            DropColumn("dbo.TaskSubItem", "FinishedById");
            DropColumn("dbo.TaskDetail", "AttachmentId");
            DropColumn("dbo.CalendarComponent", "SelectedUserId");
            DropColumn("dbo.CalendarComponent", "KntId1");
        }
    }
}
