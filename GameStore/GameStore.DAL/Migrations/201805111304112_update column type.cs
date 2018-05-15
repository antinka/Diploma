namespace GameStore.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatecolumntype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Publishers", "Name", c => c.String(maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Publishers", "Name", c => c.String());
        }
    }
}
