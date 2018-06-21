namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPictureToGameEntity : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Games", "ImageData");
            DropColumn("dbo.Games", "ImageMimeType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "ImageMimeType", c => c.String());
            AddColumn("dbo.Games", "ImageData", c => c.Binary());
        }
    }
}
