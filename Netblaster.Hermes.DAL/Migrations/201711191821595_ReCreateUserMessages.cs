namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReCreateUserMessages : DbMigration
    {
        public override void Up()
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
                        SenderDisplayName = c.String(),
                        SenderId = c.String(),
                        ReceiverId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.ReceiverId)
                .Index(t => t.ReceiverId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserMessage", "ReceiverId", "dbo.User");
            DropIndex("dbo.UserMessage", new[] { "ReceiverId" });
            DropTable("dbo.UserMessage");
        }
    }
}
