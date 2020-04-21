namespace DBAccessLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.StoredGames",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Order = c.Int(nullable: false),
                        Winner_Id = c.Int(),
                        StoredMatch_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoredPlayers", t => t.Winner_Id)
                .ForeignKey("dbo.StoredMatches", t => t.StoredMatch_Id)
                .Index(t => t.Winner_Id)
                .Index(t => t.StoredMatch_Id);
            
            CreateTable(
                "dbo.Moves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CoordinateX = c.Int(nullable: false),
                        CoordinateY = c.Int(nullable: false),
                        Order = c.Int(nullable: false),
                        StoredGame_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoredGames", t => t.StoredGame_Id)
                .Index(t => t.StoredGame_Id);
            
            CreateTable(
                "dbo.StoredPlayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StoredMatches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPlayer1PlayFirst = c.Boolean(nullable: false),
                        HasBlockTwoEndsRule = c.Boolean(nullable: false),
                        GameCount = c.Int(nullable: false),
                        Score1 = c.Int(nullable: false),
                        Score2 = c.Int(nullable: false),
                        DateTime = c.DateTime(nullable: false),
                        Player1_Id = c.Int(),
                        Player2_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StoredPlayers", t => t.Player1_Id)
                .ForeignKey("dbo.StoredPlayers", t => t.Player2_Id)
                .Index(t => t.Player1_Id)
                .Index(t => t.Player2_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoredMatches", "Player2_Id", "dbo.StoredPlayers");
            DropForeignKey("dbo.StoredMatches", "Player1_Id", "dbo.StoredPlayers");
            DropForeignKey("dbo.StoredGames", "StoredMatch_Id", "dbo.StoredMatches");
            DropForeignKey("dbo.StoredGames", "Winner_Id", "dbo.StoredPlayers");
            DropForeignKey("dbo.Moves", "StoredGame_Id", "dbo.StoredGames");
            DropIndex("dbo.StoredMatches", new[] { "Player2_Id" });
            DropIndex("dbo.StoredMatches", new[] { "Player1_Id" });
            DropIndex("dbo.Moves", new[] { "StoredGame_Id" });
            DropIndex("dbo.StoredGames", new[] { "StoredMatch_Id" });
            DropIndex("dbo.StoredGames", new[] { "Winner_Id" });
            DropTable("dbo.StoredMatches");
            DropTable("dbo.StoredPlayers");
            DropTable("dbo.Moves");
            DropTable("dbo.StoredGames");
        }
    }
}
