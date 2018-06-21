namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImageToGame : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "ImageName", c => c.String());
            AddColumn("dbo.Games", "ImageMimeType", c => c.String());
            DropColumn("dbo.Games", "PicturePath");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Games", "PicturePath", c => c.String());
            DropColumn("dbo.Games", "ImageMimeType");
            DropColumn("dbo.Games", "ImageName");
        }
    }
}
