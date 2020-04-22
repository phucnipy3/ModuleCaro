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

namespace ClassLibraryServer.UserControls
{
    /// <summary>
    /// Interaction logic for UCBoard.xaml
    /// </summary>
    public partial class UCBoard : UserControl
    {
        
        private const int BOARD_SIZE = 10;
        private int sizeOfRectangle;

        public UCBoard()
        {
            InitializeComponent();

            sizeOfRectangle = (int)this.Width / BOARD_SIZE;

            DrawBoard();
        }

        public void DrawBoard()
        {
            

            for (int i = 0; i < BOARD_SIZE; i++)
            {
                UCRectangle uCRowRectangle = new UCRectangle() { Width = sizeOfRectangle, Height = sizeOfRectangle };
                uCRowRectangle.txbChessman.Text = "";
                wpnlBoard.Children.Add(uCRowRectangle);
                for (int j = 1; j < BOARD_SIZE; j++)
                {
                    UCRectangle uCColumnRectangle = new UCRectangle() { Width = sizeOfRectangle, Height = sizeOfRectangle };
                    uCColumnRectangle.txbChessman.Text = "O";
                    wpnlBoard.Children.Add(uCColumnRectangle);
                }
            }
            

        }
    }
}
