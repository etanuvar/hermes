namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdditionalTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChatItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.TaskDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        TaskItemId = c.Int(nullable: false),
                        Text = c.String(),
                        TaskDetailType = c.Int(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUserId)
                .ForeignKey("dbo.CalendarComponent", t => t.TaskItemId)
                .Index(t => t.TaskItemId)
                .Index(t => t.ApplicationUserId);
            
            CreateTable(
                "dbo.UserMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        SenderId = c.String(maxLength: 128),
                        ReceiverId = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .ForeignKey("dbo.User", t => t.SenderId)
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .Index(t => t.SenderId)
                .Index(t => t.ReceiverId)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMessage", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.UserMessage", "SenderId", "dbo.User");
            DropForeignKey("dbo.UserMessage", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.TaskDetail", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.TaskDetail", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.ChatItem", "ApplicationUserId", "dbo.User");
            DropIndex("dbo.UserMessage", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserMessage", new[] { "ReceiverId" });
            DropIndex("dbo.UserMessage", new[] { "SenderId" });
            DropIndex("dbo.TaskDetail", new[] { "ApplicationUserId" });
            DropIndex("dbo.TaskDetail", new[] { "TaskItemId" });
            DropIndex("dbo.ChatItem", new[] { "ApplicationUserId" });
            DropTable("dbo.UserMessage");
            DropTable("dbo.TaskDetail");
            DropTable("dbo.ChatItem");
        }
    }
}
