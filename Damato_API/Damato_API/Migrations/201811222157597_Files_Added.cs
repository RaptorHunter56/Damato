namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Path = c.String(maxLength: 100),
                        DateAdded = c.DateTime(nullable: false),
                        Level = c.String(),
                    })
                .PrimaryKey(t => t.Key)
                .Index(t => t.Path, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Files", new[] { "Path" });
            DropTable("dbo.Files");
        }
    }
}
