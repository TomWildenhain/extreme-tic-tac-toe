using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ExtremeTicTacToe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private BoardManager boardManager;
        private GameManager gameManager;
        public MainWindow()
        {
            InitializeComponent();
            boardManager = new BoardManager(this);
        }

        private void playBN_Click(object sender, RoutedEventArgs e)
        {
            gameManager = new GameManager(this, boardManager);
            playBN.Visibility = Visibility.Collapsed;
            randomBN.Visibility = Visibility.Visible;
        }
        private void randomBN_Click(object sender, RoutedEventArgs e)
        {
            gameManager.randomClick();
        }
        private void resetBN_Click(object sender, RoutedEventArgs e)
        {
            gameManager = new GameManager(this, boardManager);
            playBN.Visibility = Visibility.Collapsed;
        }
        public void cell_OnClick(object sender, MouseButtonEventArgs e)
        {
            Border cellClicked = (Border)sender;
            int board = Convert.ToInt32(cellClicked.Name[1] + "");
            int square = Convert.ToInt32(cellClicked.Name[3] + "");
            int cell = Convert.ToInt32(cellClicked.Name[5] + "");
            gameManager.cellClicked(board, square, cell);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double dimension = Math.Min(sizingBorder.ActualHeight, sizingBorder.ActualWidth);
            extremeBoardHolder.Width = Math.Round(dimension);
            extremeBoardHolder.Height = Math.Round(dimension);
        }
    }

    public static class ExceptionHelper
    {
        public static int LineNumber(this Exception e)
        {

            int linenum = 0;
            try
            {
                linenum = Convert.ToInt32(e.StackTrace.Substring(e.StackTrace.LastIndexOf(":line") + 5));
            }
            catch
            {
                //Stack trace is not available!
            }
            return linenum;
        }
    }
}
