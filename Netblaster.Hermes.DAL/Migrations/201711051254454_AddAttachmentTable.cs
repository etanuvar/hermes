namespace Netblaster.Hermes.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttachmentTable : DbMigration
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
                        CalendarComponentId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CalendarComponent", t => t.CalendarComponentId)
                .Index(t => t.CalendarComponentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachment", "CalendarComponentId", "dbo.CalendarComponent");
            DropIndex("dbo.Attachment", new[] { "CalendarComponentId" });
            DropTable("dbo.Attachment");
        }
    }
}
