namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPubliserIdToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PublisherId", c => c.Guid());
            CreateIndex("dbo.Users", "PublisherId");
            AddForeignKey("dbo.Users", "PublisherId", "dbo.Publishers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "PublisherId", "dbo.Publishers");
            DropIndex("dbo.Users", new[] { "PublisherId" });
            DropColumn("dbo.Users", "PublisherId");
        }
    }
}
