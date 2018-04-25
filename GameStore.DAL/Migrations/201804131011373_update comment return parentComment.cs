namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecommentreturnparentComment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            AddColumn("dbo.Comments", "ParentCommentId", c => c.Guid());
            DropColumn("dbo.Comments", "Comment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Comment_Id", c => c.Guid());
            DropColumn("dbo.Comments", "ParentCommentId");
            CreateIndex("dbo.Comments", "Comment_Id");
            AddForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments", "Id");
        }
    }
}
