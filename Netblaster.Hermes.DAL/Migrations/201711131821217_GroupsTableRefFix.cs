namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GroupsTableRefFix : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.UserGroup", new[] { "ApplicationUser_Id" });
            DropColumn("dbo.UserGroup", "ApplicationUserId");
            RenameColumn(table: "dbo.UserGroup", name: "ApplicationUser_Id", newName: "ApplicationUserId");
            AlterColumn("dbo.UserGroup", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserGroup", "ApplicationUserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.UserGroup", new[] { "ApplicationUserId" });
            AlterColumn("dbo.UserGroup", "ApplicationUserId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserGroup", name: "ApplicationUserId", newName: "ApplicationUser_Id");
            AddColumn("dbo.UserGroup", "ApplicationUserId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserGroup", "ApplicationUser_Id");
        }
    }
}
