namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MissingColums : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TaskDetail", "AttachmentId", "dbo.Attachment");
            DropForeignKey("dbo.TaskSubItem", "AttachmentId", "dbo.Attachment");
            DropIndex("dbo.TaskDetail", new[] { "AttachmentId" });
            DropIndex("dbo.TaskSubItem", new[] { "AttachmentId" });
            AddColumn("dbo.Attachment", "TaskDetailId", c => c.Int());
            AddColumn("dbo.Attachment", "TaskSubItemId", c => c.Int());
            CreateIndex("dbo.Attachment", "TaskDetailId");
            CreateIndex("dbo.Attachment", "TaskSubItemId");
            AddForeignKey("dbo.Attachment", "TaskDetailId", "dbo.TaskDetail", "Id");
            AddForeignKey("dbo.Attachment", "TaskSubItemId", "dbo.TaskSubItem", "Id");
            DropColumn("dbo.TaskDetail", "AttachmentId");
            DropColumn("dbo.TaskSubItem", "AttachmentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TaskSubItem", "AttachmentId", c => c.Int());
            AddColumn("dbo.TaskDetail", "AttachmentId", c => c.Int());
            DropForeignKey("dbo.Attachment", "TaskSubItemId", "dbo.TaskSubItem");
            DropForeignKey("dbo.Attachment", "TaskDetailId", "dbo.TaskDetail");
            DropIndex("dbo.Attachment", new[] { "TaskSubItemId" });
            DropIndex("dbo.Attachment", new[] { "TaskDetailId" });
            DropColumn("dbo.Attachment", "TaskSubItemId");
            DropColumn("dbo.Attachment", "TaskDetailId");
            CreateIndex("dbo.TaskSubItem", "AttachmentId");
            CreateIndex("dbo.TaskDetail", "AttachmentId");
            AddForeignKey("dbo.TaskSubItem", "AttachmentId", "dbo.Attachment", "Id");
            AddForeignKey("dbo.TaskDetail", "AttachmentId", "dbo.Attachment", "Id");
        }
    }
}
