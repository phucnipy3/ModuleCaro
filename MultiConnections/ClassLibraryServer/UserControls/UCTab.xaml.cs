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

        private bool isPause;
        private bool isSkip;
        private int currentMove;
        private StoredGame storedGame;
        private UCBoard uCBoard;

        
        public UCTab(StoredGame storedGame)
        {
            this.storedGame = storedGame;
            InitializeComponent();

            uCBoard = new UCBoard();
            grdMain.Children.Add(uCBoard);
            isPause = false;
            isSkip = false;
            currentMove = -1;
        }

        private void btnPlay_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(new ThreadStart(PlayGame));
            thread.IsBackground = true;
            thread.Start();
        }

        public void PlayGame()
        {
            while (true)
            {
                if (!isPause)
                {
                    for (int i = 0; i < storedGame.Moves.Count; i++)
                    {
                        DrawChessman(i);
                        currentMove = i;
                        if (!isSkip)
                            Thread.Sleep(1000);
                    }
                    isPause = true;
                }

            }
        }

        private void btnPause_Click(object sender, RoutedEventArgs e)
        {
            isPause = true;
            btnBack.IsEnabled = true;
            btnNext.IsEnabled = true;
        }

        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            isSkip = true;
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
            
        }

        public void Back()
        {
            if (currentMove >= 0)
            {
                DeleteChessman(currentMove);
                currentMove--;
            }
            else
                currentMove = 0;
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

       
    }
}
