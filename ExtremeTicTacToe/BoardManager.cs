using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MediaColor = System.Windows.Media.Color;

namespace ExtremeTicTacToe
{
    class BoardManager
    {
        private MainWindow mainWindow;
        int[] currentItemNums = new int[2];
        Brush[,] colorsToUse = new Brush[3, 10];


        public BoardManager(MainWindow mainWindowHandle)
        {
            mainWindow = mainWindowHandle;
            mainWindow.extremeBoardHolder.Children.Add(buildExtremeBoard());
            setColors();
            //addRule(1, 0, 1, 0);
            //addRule(2, 0, 1, 0);
        }
        private void setColors()
        {
            //Bot 1 is red
            colorsToUse[0, 0] = Brushes.Red;//Rule Dockpanel fill
            colorsToUse[0, 1] = new SolidColorBrush(MediaColor.FromRgb(218, 0, 0));//Rule Dockpanel foreground
            colorsToUse[0, 2] = Brushes.White;//Rule Dockpanel text
            colorsToUse[0, 3] = Brushes.Red;//Pieces color
            colorsToUse[0, 4] = new SolidColorBrush(MediaColor.FromRgb(255, 155, 155));//Won square color
            colorsToUse[0, 5] = new SolidColorBrush(MediaColor.FromRgb(255, 91, 91));//Won square border
            //colorsToUse[0, 6] = new SolidColorBrush(MediaColor.FromRgb(255, 210, 210));//Won game color

            //Bot 2 is blue
            colorsToUse[1, 0] = new SolidColorBrush(MediaColor.FromRgb(0, 176, 240));
            colorsToUse[1, 1] = new SolidColorBrush(MediaColor.FromRgb(0, 136, 255));
            colorsToUse[1, 2] = Brushes.White;
            colorsToUse[1, 3] = new SolidColorBrush(MediaColor.FromRgb(0, 176, 240));
            colorsToUse[1, 4] = new SolidColorBrush(MediaColor.FromRgb(119, 219, 255));
            colorsToUse[1, 5] = new SolidColorBrush(MediaColor.FromRgb(64, 204, 255));
            //colorsToUse[1, 6] = new SolidColorBrush(MediaColor.FromRgb(191, 238, 255));//Won game color

            //Other colors
            colorsToUse[2, 0] = new SolidColorBrush(MediaColor.FromRgb(238, 238, 238));
            colorsToUse[2, 1] = new SolidColorBrush(MediaColor.FromRgb(255, 255, 166));
            colorsToUse[2, 3] = Brushes.White;
            colorsToUse[2, 5] = Brushes.White;
            //colorsToUse[2, 5] = new SolidColorBrush(MediaColor.FromRgb(80, 80, 80));
            //colorsToUse[2, 6] = new SolidColorBrush(MediaColor.FromRgb(0, 0, 0));//Tied game

        }
        private Border buildExtremeBoard()
        {
            Border border1 = new Border();
            border1.Name = "extremeBoard";
            mainWindow.RegisterName(border1.Name, border1);
            border1.SnapsToDevicePixels = true;
            //border1.Width = 250;
            //border1.Height = 250;
            border1.Background = new SolidColorBrush(MediaColor.FromRgb(238, 238, 238));
            border1.BorderBrush = Brushes.Black;
            border1.BorderThickness = new Thickness(4);
            UniformGrid grid1 = new UniformGrid();
            for (int i = 0; i < 9; i++)
            {
                grid1.Children.Add(buildBoard(i));
            }
            border1.Child = grid1;
            return border1;
        }
        private Border buildBoard(int boardIndex)
        {
            Border border1 = new Border();
            border1.Name = "b" + boardIndex;
            mainWindow.RegisterName(border1.Name, border1);
            border1.SnapsToDevicePixels = true;
            border1.Background = new SolidColorBrush(MediaColor.FromRgb(238, 238, 238));
            border1.BorderBrush = Brushes.Black;
            border1.BorderThickness = new Thickness(4);
            UniformGrid grid1 = new UniformGrid();
            for (int i = 0; i < 9; i++)
            {
                grid1.Children.Add(buildSquare(i, boardIndex));
            }
            border1.Child = grid1;
            return border1;
        }
        private Border buildSquare(int squareIndex, int boardIndex)
        {
            Border border1 = new Border();
            border1.Name = "b" + boardIndex + "s" + squareIndex;
            mainWindow.RegisterName(border1.Name, border1);
            border1.SnapsToDevicePixels = true;
            border1.Background = new SolidColorBrush(MediaColor.FromRgb(238, 238, 238));
            border1.BorderBrush = Brushes.DarkGray;
            border1.BorderThickness = new Thickness(2);
            UniformGrid uniformGrid1 = new UniformGrid();
            for (int i = 0; i < 9; i++)
            {
                Border currentCell = buildCell(i, squareIndex, boardIndex);
                uniformGrid1.Children.Add(currentCell);
            }
            border1.Child = uniformGrid1;
            return border1;
        }
        private Border buildCell(int cellIndex, int squareIndex, int boardIndex)
        {
            Border border1 = new Border();
            border1.Name = "b" + boardIndex + "s" + squareIndex + "c" + cellIndex;
            mainWindow.RegisterName(border1.Name, border1);
            border1.SnapsToDevicePixels = true;
            border1.Background = colorsToUse[2, 0];
            border1.BorderBrush = Brushes.LightGray;
            border1.BorderThickness = new Thickness(1);
            border1.MouseUp += new MouseButtonEventHandler(mainWindow.cell_OnClick);
            return border1;
        }
        public void updateBoard(int legalBoard, int legalSquare, int[][][] boardState, int[][] megaBoardState, int[] extremeBoardState, int winnerState, int currentBot)
        {
            Border currentEBorder = (Border)mainWindow.FindName("extremeBoard");
            currentEBorder.BorderBrush = Brushes.Black;
            if (winnerState != 0)
            {
                currentEBorder.BorderBrush = colorsToUse[winnerState - 1, 3];
            }
            for (int board = 0; board < 9; board++)
            {
                Border currentBBorder = (Border)mainWindow.FindName("b" + board);
                currentBBorder.BorderBrush = Brushes.Black;
                if (winnerState != 0)
                {
                    currentBBorder.BorderBrush = colorsToUse[winnerState - 1, 3];
                }

                for (int square = 0; square < 9; square++)
                {
                    Border currentSBorder = (Border)mainWindow.FindName("b" + board + "s" + square);
                    if (extremeBoardState[board] != 0)
                    {
                        currentSBorder.BorderBrush = colorsToUse[extremeBoardState[board] - 1, 3];
                    }
                    else
                    {
                        currentSBorder.BorderBrush = Brushes.DarkGray;
                    }
                    for (int cell = 0; cell < 9; cell++)
                    {
                        Border currentCBorder = (Border)mainWindow.FindName("b" + board + "s" + square + "c" + cell);
                        int cellVal = boardState[board][square][cell];
                        if (cellVal != 0)
                        {
                            currentCBorder.Background = colorsToUse[cellVal - 1, 3];
                        }
                        else if (megaBoardState[board][square] == 1 || megaBoardState[board][square] == 2)
                        {
                            currentCBorder.Background = colorsToUse[megaBoardState[board][square] - 1, 4];
                        }
                        else if (winnerState != 0)
                        {
                            currentCBorder.Background = colorsToUse[2, 0];
                        }
                        else if ((square == legalSquare || legalSquare == -1) && (board == legalBoard || legalBoard == -1))
                        {
                            currentCBorder.Background = colorsToUse[2, 1];//Yellow
                        }
                        else
                        {
                            currentCBorder.Background = colorsToUse[2, 0];
                        }
                        if (megaBoardState[board][square] != 0)
                        {
                            currentCBorder.BorderBrush = colorsToUse[megaBoardState[board][square] - 1, 5];
                        }
                        else
                        {
                            currentCBorder.BorderBrush = Brushes.LightGray;
                        }
                    }
                }
            }
            //end for's
        }
    }
}
