namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserGroupsAndSubTasks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TaskSubItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        IsFinished = c.Boolean(nullable: false),
                        FinishedDate = c.DateTime(),
                        TaskItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalendarComponent", t => t.TaskItemId)
                .Index(t => t.TaskItemId);
            
            AddColumn("dbo.CalendarComponent", "UserGroupId", c => c.Int());
            AddColumn("dbo.User", "UserGroupId", c => c.Int());
            CreateIndex("dbo.CalendarComponent", "UserGroupId");
            CreateIndex("dbo.User", "UserGroupId");
            AddForeignKey("dbo.User", "UserGroupId", "dbo.UserGroup", "Id");
            AddForeignKey("dbo.CalendarComponent", "UserGroupId", "dbo.UserGroup", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskSubItem", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.CalendarComponent", "UserGroupId", "dbo.UserGroup");
            DropForeignKey("dbo.User", "UserGroupId", "dbo.UserGroup");
            DropIndex("dbo.TaskSubItem", new[] { "TaskItemId" });
            DropIndex("dbo.User", new[] { "UserGroupId" });
            DropIndex("dbo.CalendarComponent", new[] { "UserGroupId" });
            DropColumn("dbo.User", "UserGroupId");
            DropColumn("dbo.CalendarComponent", "UserGroupId");
            DropTable("dbo.TaskSubItem");
            DropTable("dbo.UserGroup");
        }
    }
}
