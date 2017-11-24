namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskItemUsersAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaskItemUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        TaskItemId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUserId)
                .ForeignKey("dbo.CalendarComponent", t => t.TaskItemId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.TaskItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TaskItemUser", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.TaskItemUser", "ApplicationUserId", "dbo.User");
            DropIndex("dbo.TaskItemUser", new[] { "TaskItemId" });
            DropIndex("dbo.TaskItemUser", new[] { "ApplicationUserId" });
            DropTable("dbo.TaskItemUser");
        }
    }
}
