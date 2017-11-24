namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserMessage", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.UserMessage", "ApplicationUser_Id1", c => c.String(maxLength: 128));
            CreateIndex("dbo.UserMessage", "ApplicationUser_Id1");
            AddForeignKey("dbo.UserMessage", "ApplicationUser_Id1", "dbo.User", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMessage", "ApplicationUser_Id1", "dbo.User");
            DropIndex("dbo.UserMessage", new[] { "ApplicationUser_Id1" });
            DropColumn("dbo.UserMessage", "ApplicationUser_Id1");
            DropColumn("dbo.UserMessage", "Discriminator");
        }
    }
}
