namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FilesUser_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "User_ID", c => c.Int());
            CreateIndex("dbo.Files", "User_ID");
            AddForeignKey("dbo.Files", "User_ID", "dbo.Users", "Key");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Files", "User_ID", "dbo.Users");
            DropIndex("dbo.Files", new[] { "User_ID" });
            DropColumn("dbo.Files", "User_ID");
        }
    }
}
