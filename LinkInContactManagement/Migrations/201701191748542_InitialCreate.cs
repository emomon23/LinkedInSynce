namespace LinkInContactManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(maxLength: 100, unicode: false),
                        Email = c.String(),
                        Phone = c.String(),
                        Title = c.String(),
                        RawLinkedInName = c.String(),
                        LastAutoContacted = c.DateTime(),
                        LastReachedOut = c.DateTime(),
                        LastReachOutMethod = c.String(),
                        Notes = c.String(),
                        ScheduleNextReachOut = c.DateTime(),
                        ShouldSkipAutoReachOut = c.Boolean(nullable: false),
                        IsStillLinkedInContact = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastUpdateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId)
                .Index(t => t.LastName);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Contacts", new[] { "LastName" });
            DropTable("dbo.Contacts");
        }
    }
}
