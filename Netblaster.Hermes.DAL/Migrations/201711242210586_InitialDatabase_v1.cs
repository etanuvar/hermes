namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase_v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        BinaryData = c.Binary(),
                        FileName = c.String(),
                        MimeType = c.String(),
                        TaskItemId = c.Int(),
                        ContactId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalendarComponent", t => t.TaskItemId)
                .ForeignKey("dbo.CalendarComponent", t => t.ContactId)
                .Index(t => t.TaskItemId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.CalendarComponent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        EndDate = c.DateTime(),
                        KntId = c.Int(),
                        SelectedContactType = c.String(),
                        ApplicationUserId = c.String(maxLength: 128),
                        Title = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        DeadlineDate = c.DateTime(),
                        GroupId = c.Int(),
                        ItemStatus = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.Group", t => t.GroupId)
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.User", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CreatedById)
                .Index(t => t.GroupId)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        JobTitle = c.String(),
                        Photo = c.Binary(),
                        LastLoginDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(maxLength: 500),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(maxLength: 50),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Group",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserGroup",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        GroupId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUserId)
                .ForeignKey("dbo.Group", t => t.GroupId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.GroupId);
            
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
            
            CreateTable(
                "dbo.ToDoItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        IsReady = c.Boolean(nullable: false),
                        FinishedById = c.Int(),
                        TaskItemId = c.Int(nullable: false),
                        FinishedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.FinishedBy_Id)
                .ForeignKey("dbo.CalendarComponent", t => t.TaskItemId)
                .Index(t => t.TaskItemId)
                .Index(t => t.FinishedBy_Id);
            
            CreateTable(
                "dbo.WorkTime",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Description = c.String(),
                        TaskItemId = c.Int(),
                        ApplicationUserId = c.Int(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.CalendarComponent", t => t.TaskItemId)
                .Index(t => t.TaskItemId)
                .Index(t => t.ApplicationUser_Id);
            
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
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.User", t => t.UserId)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.UserMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Text = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        IsRead = c.Boolean(nullable: false),
                        SenderDisplayName = c.String(),
                        SenderId = c.String(),
                        ReceiverId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .Index(t => t.ReceiverId);
            
            CreateTable(
                "dbo.Parameter",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Attachment", "ContactId", "dbo.CalendarComponent");
            DropForeignKey("dbo.CalendarComponent", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.UserMessage", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.User");
            DropForeignKey("dbo.ChatItem", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.CalendarComponent", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.WorkTime", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.WorkTime", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.ToDoItem", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.ToDoItem", "FinishedBy_Id", "dbo.User");
            DropForeignKey("dbo.TaskSubItem", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.TaskItemUser", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.TaskItemUser", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.TaskDetail", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.TaskDetail", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.CalendarComponent", "GroupId", "dbo.Group");
            DropForeignKey("dbo.UserGroup", "GroupId", "dbo.Group");
            DropForeignKey("dbo.UserGroup", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.CalendarComponent", "CreatedById", "dbo.User");
            DropForeignKey("dbo.Attachment", "TaskItemId", "dbo.CalendarComponent");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.UserMessage", new[] { "ReceiverId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.ChatItem", new[] { "ApplicationUserId" });
            DropIndex("dbo.WorkTime", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.WorkTime", new[] { "TaskItemId" });
            DropIndex("dbo.ToDoItem", new[] { "FinishedBy_Id" });
            DropIndex("dbo.ToDoItem", new[] { "TaskItemId" });
            DropIndex("dbo.TaskSubItem", new[] { "TaskItemId" });
            DropIndex("dbo.TaskItemUser", new[] { "TaskItemId" });
            DropIndex("dbo.TaskItemUser", new[] { "ApplicationUserId" });
            DropIndex("dbo.TaskDetail", new[] { "ApplicationUserId" });
            DropIndex("dbo.TaskDetail", new[] { "TaskItemId" });
            DropIndex("dbo.UserGroup", new[] { "GroupId" });
            DropIndex("dbo.UserGroup", new[] { "ApplicationUserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.CalendarComponent", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CalendarComponent", new[] { "GroupId" });
            DropIndex("dbo.CalendarComponent", new[] { "CreatedById" });
            DropIndex("dbo.CalendarComponent", new[] { "ApplicationUserId" });
            DropIndex("dbo.Attachment", new[] { "ContactId" });
            DropIndex("dbo.Attachment", new[] { "TaskItemId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parameter");
            DropTable("dbo.UserMessage");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.ChatItem");
            DropTable("dbo.WorkTime");
            DropTable("dbo.ToDoItem");
            DropTable("dbo.TaskSubItem");
            DropTable("dbo.TaskItemUser");
            DropTable("dbo.TaskDetail");
            DropTable("dbo.UserGroup");
            DropTable("dbo.Group");
            DropTable("dbo.User");
            DropTable("dbo.CalendarComponent");
            DropTable("dbo.Attachment");
        }
    }
}
