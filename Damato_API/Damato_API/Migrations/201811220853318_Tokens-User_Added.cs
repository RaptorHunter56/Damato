namespace Damato_API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TokensUser_Added : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tokens", "User_ID", c => c.Int());
            CreateIndex("dbo.Tokens", "User_ID");
            AddForeignKey("dbo.Tokens", "User_ID", "dbo.Users", "Key");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tokens", "User_ID", "dbo.Users");
            DropIndex("dbo.Tokens", new[] { "User_ID" });
            DropColumn("dbo.Tokens", "User_ID");
        }
    }
}
