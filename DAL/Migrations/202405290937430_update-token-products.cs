namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatetokenproducts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImagePath", c => c.String());
            AddColumn("dbo.Products", "Stock", c => c.Int(nullable: false));
            AddColumn("dbo.Tokens", "UserRole", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tokens", "UserRole");
            DropColumn("dbo.Products", "Stock");
            DropColumn("dbo.Products", "ImagePath");
        }
    }
}
