using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using DBAccessLibary.Models;

namespace MultiConnections_WPF.UserControls
{
    /// <summary>
    /// Interaction logic for UCBoard.xaml
    /// </summary>
    public partial class UCBoard : UserControl
    {
        
        private const int BOARD_SIZE = 20;
        private const int NUMBER = 1;
        private int sizeOfRectangle;
        public UCRectangle[,] listUCRectangle = new UCRectangle[BOARD_SIZE,BOARD_SIZE];
        

        public UCBoard()
        {
            InitializeComponent();
            sizeOfRectangle = (int)this.Width / (BOARD_SIZE + NUMBER);

            DrawBoard();
            
        }

        public void DrawBoard()
        {
            for (int i = 1; i < BOARD_SIZE + NUMBER; i++)
            {
                DrawNumber(i, 0, false);
                DrawNumber(0, i, true);
            }


            for (int i = 0; i < BOARD_SIZE; i++)
            {
                DrawRectangle(i, 0);
                for (int j = 1; j < BOARD_SIZE; j++)
                {
                    DrawRectangle(i, j);
                }
            }
        }

        public void DrawNumber(int i, int j, bool isRow)
        {
            UCRectangle uCRectangle = new UCRectangle() { Width = sizeOfRectangle, Height = sizeOfRectangle };
            
            if (isRow)
            {
                uCRectangle.txbChessman.Text = (j - 1).ToString();
                uCRectangle.recBorder.StrokeThickness = 0;
                wpnlRow.Children.Add(uCRectangle);
            }
            else
            {
                uCRectangle.txbChessman.Text = (i - 1).ToString();
                uCRectangle.recBorder.StrokeThickness = 0;
                wpnlCol.Children.Add(uCRectangle);
            }
        }

        public void DrawRectangle(int i, int j)
        {
            UCRectangle uCRectangle = new UCRectangle() { Width = sizeOfRectangle, Height = sizeOfRectangle };
            listUCRectangle[i, j] = uCRectangle;
            wpnlBoard.Children.Add(uCRectangle);
        }
    }
}
