namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Tags_Edit : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Tags", name: "File_ID", newName: "File_ID_ID");
            RenameIndex(table: "dbo.Tags", name: "IX_File_ID", newName: "IX_File_ID_ID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Tags", name: "IX_File_ID_ID", newName: "IX_File_ID");
            RenameColumn(table: "dbo.Tags", name: "File_ID_ID", newName: "File_ID");
        }
    }
}
