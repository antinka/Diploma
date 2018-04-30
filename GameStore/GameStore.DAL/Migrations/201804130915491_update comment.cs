namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecomment : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "Quote", c => c.String());
            AddColumn("dbo.Comments", "Comment_Id", c => c.Guid());
            CreateIndex("dbo.Comments", "Comment_Id");
            AddForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments", "Id");
            DropColumn("dbo.Comments", "ParentCommentId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "ParentCommentId", c => c.Guid());
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropColumn("dbo.Comments", "Comment_Id");
            DropColumn("dbo.Comments", "Quote");
        }
    }
}
