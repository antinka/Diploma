namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addShippedDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ShippedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShippedDate");
        }
    }
}
