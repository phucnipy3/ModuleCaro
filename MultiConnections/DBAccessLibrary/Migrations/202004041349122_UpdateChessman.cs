namespace DBAccessLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChessman : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Moves", "Chessman", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Moves", "Chessman");
        }
    }
}
