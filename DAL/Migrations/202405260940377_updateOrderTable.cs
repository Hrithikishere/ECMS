namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItems", "Quantity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Quanity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItems", "Quanity", c => c.Int(nullable: false));
            DropColumn("dbo.OrderItems", "Quantity");
        }
    }
}
