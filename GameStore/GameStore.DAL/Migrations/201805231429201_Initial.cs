namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
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
                        Quote = c.String(),
                        GameId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.ParentCommentId)
                .Index(t => t.ParentCommentId)
                .Index(t => t.GameId);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Key = c.String(maxLength: 450),
                        Name = c.String(),
                        Description = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitsInStock = c.Short(nullable: false),
                        Discountinues = c.Boolean(nullable: false),
                        PublisherId = c.Guid(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Publishers", t => t.PublisherId)
                .Index(t => t.Key, unique: true, name: "Game_Index_Key")
                .Index(t => t.PublisherId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ParentGenreId = c.Guid(),
                        Name = c.String(maxLength: 450),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genres", t => t.ParentGenreId)
                .Index(t => t.ParentGenreId)
                .Index(t => t.Name, unique: true, name: "Genre_Index_Name");
            
            CreateTable(
                "dbo.PlatformTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 450),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "PlatformType_Index_Name");
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(maxLength: 40),
                        Description = c.String(),
                        HomePage = c.String(),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        GameId = c.Guid(nullable: false),
                        Quantity = c.Short(nullable: false),
                        Discount = c.Single(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.GameId, cascadeDelete: true)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.GameId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        UserId = c.Guid(nullable: false),
                        Date = c.DateTime(),
                        IsPaid = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderDetails", "GameId", "dbo.Games");
            DropForeignKey("dbo.Comments", "ParentCommentId", "dbo.Comments");
            DropForeignKey("dbo.Comments", "GameId", "dbo.Games");
            DropForeignKey("dbo.Games", "PublisherId", "dbo.Publishers");
            DropForeignKey("dbo.PlatformTypeGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.PlatformTypeGames", "PlatformType_Id", "dbo.PlatformTypes");
            DropForeignKey("dbo.Genres", "ParentGenreId", "dbo.Genres");
            DropForeignKey("dbo.GenreGames", "Game_Id", "dbo.Games");
            DropForeignKey("dbo.GenreGames", "Genre_Id", "dbo.Genres");
            DropIndex("dbo.PlatformTypeGames", new[] { "Game_Id" });
            DropIndex("dbo.PlatformTypeGames", new[] { "PlatformType_Id" });
            DropIndex("dbo.GenreGames", new[] { "Game_Id" });
            DropIndex("dbo.GenreGames", new[] { "Genre_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.OrderDetails", new[] { "GameId" });
            DropIndex("dbo.PlatformTypes", "PlatformType_Index_Name");
            DropIndex("dbo.Genres", "Genre_Index_Name");
            DropIndex("dbo.Genres", new[] { "ParentGenreId" });
            DropIndex("dbo.Games", new[] { "PublisherId" });
            DropIndex("dbo.Games", "Game_Index_Key");
            DropIndex("dbo.Comments", new[] { "GameId" });
            DropIndex("dbo.Comments", new[] { "ParentCommentId" });
            DropTable("dbo.PlatformTypeGames");
            DropTable("dbo.GenreGames");
            DropTable("dbo.Orders");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.Publishers");
            DropTable("dbo.PlatformTypes");
            DropTable("dbo.Genres");
            DropTable("dbo.Games");
            DropTable("dbo.Comments");
        }
    }
}
