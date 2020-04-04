namespace DBAccessLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChessman1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Moves", "Chessman", c => c.String(maxLength: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Moves", "Chessman", c => c.String());
        }
    }
}
