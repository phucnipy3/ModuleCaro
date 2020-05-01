namespace DBAccessLibrary.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFK : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Moves", name: "StoredGame_Id", newName: "Game_Id");
            RenameColumn(table: "dbo.StoredGames", name: "StoredMatch_Id", newName: "Match_Id");
            RenameIndex(table: "dbo.StoredGames", name: "IX_StoredMatch_Id", newName: "IX_Match_Id");
            RenameIndex(table: "dbo.Moves", name: "IX_StoredGame_Id", newName: "IX_Game_Id");
            AddColumn("dbo.StoredMatches", "StoredPlayer_Id", c => c.Int());
            CreateIndex("dbo.StoredMatches", "StoredPlayer_Id");
            AddForeignKey("dbo.StoredMatches", "StoredPlayer_Id", "dbo.StoredPlayers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoredMatches", "StoredPlayer_Id", "dbo.StoredPlayers");
            DropIndex("dbo.StoredMatches", new[] { "StoredPlayer_Id" });
            DropColumn("dbo.StoredMatches", "StoredPlayer_Id");
            RenameIndex(table: "dbo.Moves", name: "IX_Game_Id", newName: "IX_StoredGame_Id");
            RenameIndex(table: "dbo.StoredGames", name: "IX_Match_Id", newName: "IX_StoredMatch_Id");
            RenameColumn(table: "dbo.StoredGames", name: "Match_Id", newName: "StoredMatch_Id");
            RenameColumn(table: "dbo.Moves", name: "Game_Id", newName: "StoredGame_Id");
        }
    }
}
