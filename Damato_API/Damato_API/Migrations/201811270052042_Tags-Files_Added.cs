namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TagsFiles_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tags", "File_ID", c => c.Int());
            CreateIndex("dbo.Tags", "File_ID");
            AddForeignKey("dbo.Tags", "File_ID", "dbo.Files", "Key");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "File_ID", "dbo.Files");
            DropIndex("dbo.Tags", new[] { "File_ID" });
            DropColumn("dbo.Tags", "File_ID");
        }
    }
}
