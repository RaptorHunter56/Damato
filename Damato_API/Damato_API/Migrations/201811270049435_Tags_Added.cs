namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        _Tag = c.String(),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tags");
        }
    }
}
