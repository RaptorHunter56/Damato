namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tokens_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tokens",
                c => new
                    {
                        _Token = c.String(nullable: false, maxLength: 10),
                        DateAdded = c.DateTime(nullable: false),
                        DateExpiered = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t._Token)
                .Index(t => t._Token, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tokens", new[] { "_Token" });
            DropTable("dbo.Tokens");
        }
    }
}
