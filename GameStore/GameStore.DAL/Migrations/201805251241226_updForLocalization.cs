namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdForLocalization : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Genres", "Genre_Index_Name");
            DropIndex("dbo.PlatformTypes", "PlatformType_Index_Name");
            AddColumn("dbo.Games", "NameEn", c => c.String());
            AddColumn("dbo.Games", "NameRu", c => c.String());
            AddColumn("dbo.Games", "DescriptionEn", c => c.String());
            AddColumn("dbo.Games", "DescriptionRu", c => c.String());
            AddColumn("dbo.Genres", "NameEn", c => c.String(maxLength: 450));
            AddColumn("dbo.Genres", "NameRu", c => c.String(maxLength: 450));
            AddColumn("dbo.PlatformTypes", "NameEn", c => c.String(maxLength: 450));
            AddColumn("dbo.PlatformTypes", "NameRu", c => c.String(maxLength: 450));
            AddColumn("dbo.Publishers", "DescriptionEn", c => c.String());
            AddColumn("dbo.Publishers", "DescriptionRu", c => c.String());
            CreateIndex("dbo.Genres", "NameEn", unique: true, name: "Genre_Index_Name");
            CreateIndex("dbo.PlatformTypes", "NameEn", unique: true, name: "PlatformType_Index_Name");
            DropColumn("dbo.Games", "Name");
            DropColumn("dbo.Games", "Description");
            DropColumn("dbo.Genres", "Name");
            DropColumn("dbo.PlatformTypes", "Name");
            DropColumn("dbo.Publishers", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Publishers", "Description", c => c.String());
            AddColumn("dbo.PlatformTypes", "Name", c => c.String(maxLength: 450));
            AddColumn("dbo.Genres", "Name", c => c.String(maxLength: 450));
            AddColumn("dbo.Games", "Description", c => c.String());
            AddColumn("dbo.Games", "Name", c => c.String());
            DropIndex("dbo.PlatformTypes", "PlatformType_Index_Name");
            DropIndex("dbo.Genres", "Genre_Index_Name");
            DropColumn("dbo.Publishers", "DescriptionRu");
            DropColumn("dbo.Publishers", "DescriptionEn");
            DropColumn("dbo.PlatformTypes", "NameRu");
            DropColumn("dbo.PlatformTypes", "NameEn");
            DropColumn("dbo.Genres", "NameRu");
            DropColumn("dbo.Genres", "NameEn");
            DropColumn("dbo.Games", "DescriptionRu");
            DropColumn("dbo.Games", "DescriptionEn");
            DropColumn("dbo.Games", "NameRu");
            DropColumn("dbo.Games", "NameEn");
            CreateIndex("dbo.PlatformTypes", "Name", unique: true, name: "PlatformType_Index_Name");
            CreateIndex("dbo.Genres", "Name", unique: true, name: "Genre_Index_Name");
        }
    }
}
