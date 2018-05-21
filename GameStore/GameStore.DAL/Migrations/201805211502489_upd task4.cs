namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updtask4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ShipperId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShipperId");
        }
    }
}
