namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Groups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.User", "UserGroupId", "dbo.UserGroup");
            DropIndex("dbo.User", new[] { "UserGroupId" });
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
            
            AddColumn("dbo.UserGroup", "ApplicationUserId", c => c.Int(nullable: false));
            AddColumn("dbo.UserGroup", "GroupId", c => c.Int(nullable: false));
            AddColumn("dbo.UserGroup", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserGroup", "GroupId");
            CreateIndex("dbo.UserGroup", "ApplicationUser_Id");
            AddForeignKey("dbo.UserGroup", "ApplicationUser_Id", "dbo.User", "Id");
            AddForeignKey("dbo.UserGroup", "GroupId", "dbo.Group", "Id");
            DropColumn("dbo.User", "UserGroupId");
            DropColumn("dbo.UserGroup", "CreateDate");
            DropColumn("dbo.UserGroup", "IsActive");
            DropColumn("dbo.UserGroup", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserGroup", "Name", c => c.String());
            AddColumn("dbo.UserGroup", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.UserGroup", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.User", "UserGroupId", c => c.Int());
            DropForeignKey("dbo.UserGroup", "GroupId", "dbo.Group");
            DropForeignKey("dbo.UserGroup", "ApplicationUser_Id", "dbo.User");
            DropIndex("dbo.UserGroup", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserGroup", new[] { "GroupId" });
            DropColumn("dbo.UserGroup", "ApplicationUser_Id");
            DropColumn("dbo.UserGroup", "GroupId");
            DropColumn("dbo.UserGroup", "ApplicationUserId");
            DropTable("dbo.Group");
            CreateIndex("dbo.User", "UserGroupId");
            AddForeignKey("dbo.User", "UserGroupId", "dbo.UserGroup", "Id");
        }
    }
}
