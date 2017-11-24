namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalendarComponent",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreateDate = c.DateTime(nullable: false),
                        Note = c.String(),
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        EndDate = c.DateTime(),
                        Title = c.String(),
                        CreatedById = c.String(maxLength: 128),
                        KntId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.User", t => t.CreatedById)
                .ForeignKey("dbo.User", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.CreatedById)
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
            DropForeignKey("dbo.CalendarComponent", "ApplicationUserId", "dbo.User");
            DropForeignKey("dbo.WorkTime", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.ToDoItem", "TaskItemId", "dbo.CalendarComponent");
            DropForeignKey("dbo.ToDoItem", "FinishedBy_Id", "dbo.User");
            DropForeignKey("dbo.CalendarComponent", "CreatedById", "dbo.User");
            DropForeignKey("dbo.WorkTime", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.User");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.User");
            DropForeignKey("dbo.CalendarComponent", "ApplicationUser_Id", "dbo.User");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ToDoItem", new[] { "FinishedBy_Id" });
            DropIndex("dbo.ToDoItem", new[] { "TaskItemId" });
            DropIndex("dbo.WorkTime", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.WorkTime", new[] { "TaskItemId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.User", "UserNameIndex");
            DropIndex("dbo.CalendarComponent", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.CalendarComponent", new[] { "CreatedById" });
            DropIndex("dbo.CalendarComponent", new[] { "ApplicationUserId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Parameter");
            DropTable("dbo.ToDoItem");
            DropTable("dbo.WorkTime");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.User");
            DropTable("dbo.CalendarComponent");
        }
    }
}
