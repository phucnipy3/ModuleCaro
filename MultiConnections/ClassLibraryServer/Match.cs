using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using DBAccessLibrary.DBHelper;
using DBAccessLibary.Models;

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
        private StoredMatch storedMatch;

        public Player Player1 { get => player1; set => player1 = value; }
        public Player Player2 { get => player2; set => player2 = value; }

        public bool IsPlayer1PlayFirst { get { return player1 == firstPlayer; } }

        public bool IsBlockBothEnds { get => isBlockBothEnds; set => isBlockBothEnds = value; }

        public Match()
        {

        }

        public Match(Player player1, Player player2, int maxGames = 1, bool isBlockBothEnds = false)
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

            storedMatch = Helper.AddNewMatch(Helper.GetPlayerByName(Player1.Name), Helper.GetPlayerByName(Player2.Name), IsPlayer1PlayFirst, IsBlockBothEnds, maxGames);

        }



        public void TryStartAllGames()
        {
            try
            {
                StartAllGames();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message + "\r\n"+e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void StartAllGames()
        {
            while(gamesCounter<=maxGames)
            {
                StartOneGame();
            }
        }

        public void TryStartOneGame()
        {
            try
            {
                StartOneGame();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message+"\r\n" + e.StackTrace, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartOneGame()
        {
           
            Game game = new Game(firstPlayer, secondPlayer, storedMatch);
            game.Start();
            IncreaseScoreOf(game.Winner);
            SwapPlayers();
            gamesCounter++;
            ShowScore();
        }

        public string ShowScore()
        {
            return scoreOfPlayer1 + " : " + scoreOfPlayer2;
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
        public void SwapPlayers()
        {
            Player tempPlayer = firstPlayer;
            firstPlayer = secondPlayer;
            secondPlayer = tempPlayer;
            Helper.SwapPlayer(storedMatch);
        }
    }
}
