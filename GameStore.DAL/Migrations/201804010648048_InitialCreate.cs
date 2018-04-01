namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Body = c.String(),
                        ParentCommentId = c.Guid(),
                        GameId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(maxLength: 450),
                        Name = c.String(),
                        Description = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Key, unique: true, name: "Index_Key");
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdParentGanre = c.Guid(),
                        Name = c.String(maxLength: 450),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "Index_Name");
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(maxLength: 450),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Type, unique: true, name: "Index_Type");
            
            CreateTable(
                "dbo.GenreGames",
                c => new
                    {
                        Genre_Id = c.Guid(nullable: false),
                        Game_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_Id, t.Game_Id })
                .ForeignKey("dbo.Genres", t => t.Genre_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.Genre_Id)
                .Index(t => t.Game_Id);
            
            CreateTable(
                "dbo.PlatformTypeGames",
                c => new
                    {
                        PlatformType_Id = c.Guid(nullable: false),
                        Game_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PlatformType_Id, t.Game_Id })
                .ForeignKey("dbo.PlatformTypes", t => t.PlatformType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .Index(t => t.PlatformType_Id)
                .Index(t => t.Game_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "PlatformType_Id", "dbo.PlatformTypes");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.PlatformTypeGames", new[] { "Game_Id" });
            DropIndex("dbo.PlatformTypeGames", new[] { "PlatformType_Id" });
            DropIndex("dbo.GenreGames", new[] { "Game_Id" });
            DropIndex("dbo.GenreGames", new[] { "Genre_Id" });
            DropIndex("dbo.PlatformTypes", "Index_Type");
            DropIndex("dbo.Genres", "Index_Name");
            DropIndex("dbo.Games", "Index_Key");
            DropIndex("dbo.Comments", new[] { "GameId" });
            DropTable("dbo.PlatformTypeGames");
            DropTable("dbo.GenreGames");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
        }
    }
}
