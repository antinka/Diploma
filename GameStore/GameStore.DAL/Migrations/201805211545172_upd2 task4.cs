namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class upd2task4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "PublishDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ShipperId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShipperId");
            DropColumn("dbo.Games", "Views");
            DropColumn("dbo.Games", "PublishDate");
        }
    }
}
