namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Users_Added : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Key = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        Password = c.String(),
                        Level = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Key)
                .Index(t => t.Name, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Name" });
            DropTable("dbo.Users");
        }
    }
}
