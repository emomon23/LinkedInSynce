namespace LinkInContactManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddModel_ToDoList : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ToDoes",
                c => new
                    {
                        ToDoId = c.Int(nullable: false, identity: true),
                        Description = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DueDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ToDoId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ToDoes");
        }
    }
}
