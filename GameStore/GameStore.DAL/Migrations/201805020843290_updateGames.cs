namespace GameStore.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updateGames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Games", "Views", c => c.Int(nullable: false));
            AddColumn("dbo.Games", "PublishDate", c => c.DateTime(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.Games", "PublishDate");
            DropColumn("dbo.Games", "Views");
        }
    }
}
