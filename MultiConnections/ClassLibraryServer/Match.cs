using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DBAccessLibrary.DBHelper;
using DBAccessLibary.Models;
using System.Threading;

namespace ClassLibraryServer
{
    public class Match
    {
        private Player player1;
        private Player player2;
        private Player firstPlayer;
        private Player secondPlayer; 
        private bool isBlockBothEnds;
        private int maxGames;
        private int gamesCounter;
        private int scoreOfPlayer1;
        private int scoreOfPlayer2;
        private bool gameEnded;
        private int storedMatchId;

        public Player Player1 { get => player1; set => player1 = value; }
        public Player Player2 { get => player2; set => player2 = value; }

        public bool IsPlayer1PlayFirst { get { return player1 == firstPlayer; } }

        public bool IsBlockBothEnds { get => isBlockBothEnds; set => isBlockBothEnds = value; }
        public int StoredMatchId { get => storedMatchId; set => storedMatchId = value; }
        public int MaxGames { get => maxGames;}

        public Match()
        {

        }

        private Match(Player player1, Player player2, int maxGames = 1, bool isBlockBothEnds = false)
        {
            this.Player1 = player1;
            this.Player2 = player2;
            firstPlayer = player1;
            secondPlayer = player2;
            this.IsBlockBothEnds = isBlockBothEnds;
            this.maxGames = maxGames;
            scoreOfPlayer1 = scoreOfPlayer2 = 0;
            gamesCounter = 1;
            gameEnded = false;

        }

        public static async Task<Match> CreateNewMatchAsync(Player player1, Player player2, int maxGames = 1, bool isBlockBothEnds = false)
        {
            Match match = new Match(player1, player2, maxGames, isBlockBothEnds);
            match.StoredMatchId = await Helper.AddNewMatchAsync(Helper.GetPlayerIdByName(match.Player1.Name), Helper.GetPlayerIdByName(match.Player2.Name), match.IsPlayer1PlayFirst, match.IsBlockBothEnds, match.maxGames);
            return match;
        }

        public async Task TryStartAllGamesAsync()
        {
            try
            {
                await StartAllGamesAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n"+e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private async Task StartAllGamesAsync()
        {
            while(gamesCounter<=MaxGames)
            {
                await StartOneGameAsync();
            }
        }

        public async Task TryStartOneGameAsync()
        {
            try
            {
                await StartOneGameAsync();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message+"\r\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async Task StartOneGameAsync()
        {
           
            Game game = await Game.CreateNewGameAsync(firstPlayer, secondPlayer, StoredMatchId);
            await game.StartAsync();
            IncreaseScoreOf(game.Winner);
            await SwapPlayersAsync();
            gamesCounter++;
            NotifyScoreChanged();
        }

        public void NotifyScoreChanged()
        {
            ScoreChangedEventArgs args = new ScoreChangedEventArgs();
            args.ScoreOfPlayer1 = scoreOfPlayer1;
            args.ScoreOfPlayer2 = scoreOfPlayer2;
            OnScoreChanged(args);
        }
        private void IncreaseScoreOf(Player winner)
        {
            if (winner == Player1)
                scoreOfPlayer1++;
            else if(winner == Player2)
                scoreOfPlayer2++;
            else
            {
                throw new Exception("Winner is either players");
            }
        }
        public async Task SwapPlayersAsync()
        {
            Player tempPlayer = firstPlayer;
            firstPlayer = secondPlayer;
            secondPlayer = tempPlayer;
            await Helper.SwapPlayerAsync(StoredMatchId);
        }
        protected virtual void OnScoreChanged(ScoreChangedEventArgs e)
        {
            EventHandler<ScoreChangedEventArgs> handler = ScoreChanged;
            if(handler != null)
            {
                handler(this, e);
            }    
        }
        public EventHandler<ScoreChangedEventArgs> ScoreChanged;
    }
    public class ScoreChangedEventArgs : EventArgs
    {
        public int ScoreOfPlayer1 { get; set; }
        public int ScoreOfPlayer2 { get; set; }
    }
}
