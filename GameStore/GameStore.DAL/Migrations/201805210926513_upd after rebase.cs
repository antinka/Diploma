namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updafterrebase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "PublishDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Games", "Views", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "Views");
            DropColumn("dbo.Games", "PublishDate");
        }
    }
}
