using DBAccessLibary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ClassLibraryServer.UserControls
{
    /// <summary>
    /// Interaction logic for UCTab.xaml
    /// </summary>
    public partial class UCTab : UserControl
    {

        
        private int currentMove;
        private StoredGame storedGame;
        private UCBoard uCBoard;

        private int delay;
        private bool isPause;
        private bool isOver;

        public UCTab(StoredGame storedGame)
        {
            this.storedGame = storedGame;
            InitializeComponent();

            uCBoard = new UCBoard();
            grdMain.Children.Add(uCBoard);
            isPause = true;
            isOver = false;
            currentMove = -1;

            Thread thread = new Thread(new ThreadStart(PlayGame));
            thread.IsBackground = true;
            thread.Start();

            delay = 1000;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            if (isOver)
                ResetGame();
            isPause = false;
            btnBack.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnPlay.Visibility = Visibility.Collapsed;
            btnPause.Visibility = Visibility.Visible;
        }

        public void PlayGame()
        {
            while (true)
            {
                if (!isPause)
                {
                    Next();
                    Thread.Sleep(delay);
                }
                else
                {
                    Thread.Sleep(200);
                }   
            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            isPause = true;
            btnBack.IsEnabled = true;
            btnNext.IsEnabled = true;
            btnPlay.Visibility = Visibility.Visible;
            btnPause.Visibility = Visibility.Collapsed;
        }

        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            delay = 0;
            isPause = false;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Back();
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            Next();
        }

        

        public void Next()
        {
            if (currentMove < storedGame.Moves.Count - 1)
            {
                currentMove++;
                DrawChessman(currentMove);
            }
            else
            {
                isOver = true;
            }
            
        }

        public void Back()
        {
            isOver = false;
            if (currentMove >= 0)
            {
                DeleteChessman(currentMove);
                currentMove--;
            }
            else
                currentMove = 0;
        }

        public void ResetGame()
        {
            isPause = true;
            isOver = false;
            currentMove = -1;
            delay = 1000;
            btnBack.IsEnabled = false;
            btnNext.IsEnabled = false;
            btnPause.Visibility = Visibility.Collapsed;
            btnPlay.Visibility = Visibility.Visible;
            foreach (var chessMan in uCBoard.listUCRectangle)
                chessMan.txbChessman.Text = "";
        }

        void DeleteChessman(int index)
        {
            int x = storedGame.Moves[index].CoordinateX;
            int y = storedGame.Moves[index].CoordinateY;
            uCBoard.listUCRectangle[x, y].txbChessman.Text = "";
        }

        void DrawChessman(int index)
        {

            int x = storedGame.Moves[index].CoordinateX;
            int y = storedGame.Moves[index].CoordinateY;
            string chessMan = storedGame.Moves[index].Chessman;

            uCBoard.Dispatcher.BeginInvoke(new Action(delegate {
                uCBoard.listUCRectangle[x, y].txbChessman.Text = chessMan;
            }));

            
            if (chessMan == "X")
                uCBoard.Dispatcher.BeginInvoke(new Action(delegate {
                    uCBoard.listUCRectangle[x, y].Foreground = new SolidColorBrush(Colors.Red);
                }));
            if (chessMan == "O")
                uCBoard.Dispatcher.BeginInvoke(new Action(delegate {
                    uCBoard.listUCRectangle[x, y].Foreground = new SolidColorBrush(Colors.Blue);
                }));
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetGame();
        }
    }
}
