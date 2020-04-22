using DBAccessLibary.DBAccess;
using DBAccessLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBAccessLibrary.DBHelper
{
    public static class Helper
    {
        public static ModuleContext DB = new ModuleContext();
        
        public static StoredMatch AddNewMatch(
            StoredPlayer player1, 
            StoredPlayer player2, 
            bool isPlayer1PlayFirst, 
            bool blockTwoEndsRule, 
            int gameCount
            )
        {
            StoredMatch match = new StoredMatch()
            {
                Player1 = player1,
                Player2 = player2,
                IsPlayer1PlayFirst = isPlayer1PlayFirst,
                HasBlockTwoEndsRule = blockTwoEndsRule,
                GameCount = gameCount,
                DateTime = DateTime.Now
            };
            DB.Matches.Add(match);
            DB.SaveChanges();
            return match;
        }
        public static StoredPlayer GetPlayerByName(string name)
        {
            return DB.Players.Where(x => x.Username == name).SingleOrDefault();
        }
        public static StoredGame AddNewGameToMatch(StoredMatch match)
        {
            StoredGame game = new StoredGame();
            match.Games.Add(game);
            game.Order = match.Games.Count;
            DB.SaveChanges();
            return game;
        }
        public static void AddMove(StoredGame game, string chessman, int coordinateX, int coordinateY)
        {
            Move move = new Move()
            {
                Chessman = chessman,
                CoordinateX = coordinateX,
                CoordinateY = coordinateY
            };
            game.Moves.Add(move);
            move.Order = game.Moves.Count;
            DB.SaveChanges();
        }
        public static void SetWinner(StoredPlayer winner, StoredGame game)
        {
            game.Winner = winner;
            DB.SaveChanges();
        }

        public static void SwapPlayer(StoredMatch storedMatch)
        {
            if (storedMatch.GameCount < 1)
            {
                storedMatch.IsPlayer1PlayFirst = !storedMatch.IsPlayer1PlayFirst;
                DB.SaveChanges();
            }
        }
        
        public static bool Login(string infomation)
        {
            var players = DB.Players;
            return players.Where(x => infomation.Equals("[username]"+x.Username+"[password]"+x.Password+"[end]")).Count() > 0;
        }
        public static bool AddPlayer(string username, string password)
        {
            if (DB.Players.Where(x => x.Username == username).Count() > 0)
                return false;
            StoredPlayer player = new StoredPlayer() { Username = username, Password = password };
            DB.Players.Add(player);
            DB.SaveChanges();
            return true;
        }

        public static StoredMatch GetMatch(int id)
        {
            return DB.Matches.Include("Player1").Include("Player2").Include("Games.Moves").Where(x => x.Id == id).SingleOrDefault();
        }

        public static void CheckOnSomething()
        {
            var x = DB.Matches.Include("Player1").Include("Player2").Include("Games.Moves").First();
            return;
        }
    }
}
