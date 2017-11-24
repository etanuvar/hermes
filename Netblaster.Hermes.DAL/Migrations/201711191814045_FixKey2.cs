namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixKey2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserMessage", "ApplicationUser_Id", "dbo.User");
            DropForeignKey("dbo.UserMessage", "ApplicationUser_Id1", "dbo.User");
            DropForeignKey("dbo.UserMessage", "ReceiverId", "dbo.User");
            DropForeignKey("dbo.UserMessage", "SenderId", "dbo.User");
            DropIndex("dbo.UserMessage", new[] { "SenderId" });
            DropIndex("dbo.UserMessage", new[] { "ReceiverId" });
            DropIndex("dbo.UserMessage", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.UserMessage", new[] { "ApplicationUser_Id1" });
            DropTable("dbo.UserMessage");
        }
        
        public override void Down()
        {
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        ApplicationUser_Id1 = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.UserMessage", "ApplicationUser_Id1");
            CreateIndex("dbo.UserMessage", "ApplicationUser_Id");
            CreateIndex("dbo.UserMessage", "ReceiverId");
            CreateIndex("dbo.UserMessage", "SenderId");
            AddForeignKey("dbo.UserMessage", "SenderId", "dbo.User", "Id");
            AddForeignKey("dbo.UserMessage", "ReceiverId", "dbo.User", "Id");
            AddForeignKey("dbo.UserMessage", "ApplicationUser_Id1", "dbo.User", "Id");
            AddForeignKey("dbo.UserMessage", "ApplicationUser_Id", "dbo.User", "Id");
        }
    }
}
