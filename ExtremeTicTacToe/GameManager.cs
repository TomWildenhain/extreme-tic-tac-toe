using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExtremeTicTacToe
{
    class GameManager
    {
        private MainWindow mainWindow;
        private BoardManager boardManager;
        private int legalSquare = -1;
        private int legalBoard = -1;
        public int currentBot = 1;
        private int[][][] boardState = new int[9][][];//[outerRow,outerColumn][innerRow,innerColumn]
        private int[][] megaBoardState = new int[9][];//[row,column]
        private int[] extremeBoardState = new int[9];//[row,column]
        private int winnerState = 0;
        Random rand = new Random();

        public GameManager(MainWindow mainWindowHandle, BoardManager boardManagerHandle)
        {
            mainWindow = mainWindowHandle;
            boardManager = boardManagerHandle;
            initializeBoard();
            startGame();
        }
        public void cellClicked(int board, int square, int cell)
        {
            if (checkMove(board, square, cell))
            {
                makeMove(board, square, cell, currentBot);
                boardManager.updateBoard(legalBoard, legalSquare, boardState, megaBoardState, extremeBoardState, winnerState, currentBot);
            }
        }//updated
        public void randomClick()
        {
            if (winnerState != 0) return;
            int board = legalBoard;
            while (board == -1 || extremeBoardState[board] != 0) { board = rand.Next(0, 9); }
            int square = legalSquare;
            while (square == -1 || megaBoardState[board][square] != 0) { square = rand.Next(0, 9); }
            int cell = rand.Next(0, 9);
            while (boardState[board][square][cell] != 0) { cell = rand.Next(0, 9); }
            cellClicked(board, square, cell);
        }
        private void makeMove2(int board, int square, int cell, int botNum)
        {
            boardState[board][square][cell] = botNum;
            legalSquare = cell;
            legalBoard = square;
            megaBoardState[board][square] = checkFor3InRow(boardState[board][square]);
            extremeBoardState[board] = checkFor3InRow(megaBoardState[board]);
            winnerState = checkFor3InRow(extremeBoardState);
            if (extremeBoardState[legalBoard] != 0)
            {
                legalBoard = board;
                if (extremeBoardState[legalBoard] != 0)
                {
                    legalBoard = -1;//It gives free movement
                }
            }
            if (legalBoard == -1) { legalSquare = -1; }
            else if (megaBoardState[legalBoard][legalSquare] != 0)
            {
                legalSquare = square;
                if (megaBoardState[legalBoard][legalSquare] != 0)
                {
                    legalSquare = -1;//It gives free movement
                }
            }

            currentBot = otherBot(currentBot);
        }
        private void makeMove(int board, int square, int cell, int botNum)
        {
            boardState[board][square][cell] = botNum;
            megaBoardState[board][square] = checkFor3InRow(boardState[board][square]);
            extremeBoardState[board] = checkFor3InRow(megaBoardState[board]);
            winnerState = checkFor3InRow(extremeBoardState);

            legalSquare = cell;
            if (megaBoardState[board][legalSquare] != 0)
            {
                legalSquare = square;
                if (megaBoardState[board][legalSquare] != 0)
                {
                    legalSquare = -1;
                }
            }

            if (megaBoardState[board][square] != 0)
            {
                legalBoard = square;
                legalSquare = -1;
                if (extremeBoardState[legalBoard] != 0)
                {
                    legalBoard = board;
                    if (extremeBoardState[legalBoard] != 0)
                    {
                        legalBoard = -1;
                    }
                }
            }
            else
            {
                legalBoard = board;
            }

            currentBot = otherBot(currentBot);
        }
        private bool checkMove(int board, int square, int cell)
        {
            return winnerState == 0 && (board == legalBoard || legalBoard == -1) && (square == legalSquare || legalSquare == -1) && boardState[board][square][cell] == 0 && megaBoardState[board][square] == 0 && extremeBoardState[board] == 0;
        }//Updated
        private int checkFor3InRow(int[] board)
        {
            string[] patterns = new string[8];
            patterns[0] = "100100100";
            patterns[1] = "010010010";
            patterns[2] = "001001001";
            patterns[3] = "111000000";
            patterns[4] = "000111000";
            patterns[5] = "000000111";
            patterns[6] = "100010001";
            patterns[7] = "001010100";
            int[] bot1qualifyList = new int[8];
            int[] bot2qualifyList = new int[8];
            bool full = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (patterns[j][i] == '1')
                    {
                        if (board[i] != 1)
                        {
                            bot1qualifyList[j] = 1;
                        }
                        if (board[i] != 2)
                        {
                            bot2qualifyList[j] = 1;
                        }
                    }
                }
                if (board[i] == 0)
                {
                    full = false;
                }
            }
            bool bot1qualify = bot1qualifyList.Sum() < bot1qualifyList.Length;
            bool bot2qualify = bot2qualifyList.Sum() < bot2qualifyList.Length;
            if (bot1qualify) return 1;
            if (bot2qualify) return 2;
            if (full) return 3;
            return 0;
        }

        private void initializeBoard()
        {
            legalSquare = -1;
            legalBoard = -1;
            winnerState = 0;
            boardState = new int[9][][];
            megaBoardState = new int[9][];
            extremeBoardState = new int[9];
            for (int board = 0; board < 9; board++)
            {
                boardState[board] = new int[9][];
                megaBoardState[board] = new int[9];
                for (int square = 0; square < 9; square++)
                {
                    boardState[board][square] = new int[9];
                }
            }
        }
        public void startGame()
        {
            boardManager.updateBoard(legalBoard, legalSquare, boardState, megaBoardState, extremeBoardState, winnerState, currentBot);
        }
        private int otherBot(int botNum)
        {
            int res = botNum + 1;
            if (res == 3) res = 1;
            return res;
        }
    }
}
