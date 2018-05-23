namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class extendordertomongoorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "ShipVia", c => c.Int());
            AddColumn("dbo.Orders", "Freight", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ShipName", c => c.String());
            AddColumn("dbo.Orders", "ShipAddress", c => c.String());
            AddColumn("dbo.Orders", "ShipCity", c => c.String());
            AddColumn("dbo.Orders", "ShipRegion", c => c.String());
            AddColumn("dbo.Orders", "ShipPostalCode", c => c.String());
            AddColumn("dbo.Orders", "ShipCountry", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Orders", "ShipCountry");
            DropColumn("dbo.Orders", "ShipPostalCode");
            DropColumn("dbo.Orders", "ShipRegion");
            DropColumn("dbo.Orders", "ShipCity");
            DropColumn("dbo.Orders", "ShipAddress");
            DropColumn("dbo.Orders", "ShipName");
            DropColumn("dbo.Orders", "Freight");
            DropColumn("dbo.Orders", "ShipVia");
        }
    }
}
