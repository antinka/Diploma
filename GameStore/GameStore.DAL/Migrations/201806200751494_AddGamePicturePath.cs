namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGamePicturePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "PicturePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Games", "PicturePath");
        }
    }
}
