namespace LinkInContactManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyToDoModel_Add_DateCompleted_And_Title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ToDoes", "Title", c => c.String());
            AddColumn("dbo.ToDoes", "DateCompleted", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ToDoes", "DateCompleted");
            DropColumn("dbo.ToDoes", "Title");
        }
    }
}
