using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game2048
{
    class Game2048
    {
        static int[,] board = new int[4, 4];
        static int[,] tempBoard = new int[board.GetUpperBound(0) + 1, board.GetUpperBound(1) + 1];
        static int randomRow;
        static int randomCol;

        static void Main(string[] args)
        {
            InitializeBoard();
            Spawn();

            string input = Console.ReadLine();
            if (input == "start")
            {
                do
                {
                    Spawn();
                    PrintBoard();
                    CopyBoard();

                input:
                    input = Console.ReadLine();
                    switch (input)
                    {
                        case "right":
                            for (int row = 0; row <= board.GetUpperBound(0); row++)
                            {
                                for (int col = 0; col < board.GetUpperBound(1); col++)
                                {
                                    if (board[row, col + 1] == 0 || board[row, col + 1] == board[row, col])
                                    {
                                        board[row, col + 1] += board[row, col];
                                        if (col == 0)
                                        {
                                            board[row, col] = 0;
                                        }
                                        else
                                        {
                                            board[row, col] = board[row, col - 1];
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (ZeroChecker(row, input))
                                {
                                    row--;
                                }
                            }
                            if (AreBoardsSame()) 
                            {
                                goto input;
                            }
                            break;

                        case "left":
                            for (int row = 0; row <= board.GetUpperBound(0); row++)
                            {
                                for (int col = board.GetUpperBound(1); col > 0; col--)
                                {
                                    if (board[row, col - 1] == 0 || board[row, col - 1] == board[row, col])
                                    {
                                        board[row, col - 1] += board[row, col];
                                        if (col == board.GetUpperBound(1))
                                        {
                                            board[row, col] = 0;
                                        }
                                        else
                                        {
                                            board[row, col] = board[row, col + 1];
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (ZeroChecker(row, input))
                                {
                                    row--;
                                }
                            }
                            if (AreBoardsSame())
                            {
                                goto input;
                            }
                            break;

                        case "down":
                            for (int col = 0; col <= board.GetUpperBound(1); col++)
                            {
                                for (int row = 0; row < board.GetUpperBound(0); row++)
                                {
                                    if (board[row + 1, col] == 0 || board[row + 1, col] == board[row, col])
                                    {
                                        board[row + 1, col] += board[row, col];
                                        if (row == 0)
                                        {
                                            board[row, col] = 0;
                                        }
                                        else
                                        {
                                            board[row, col] = board[row - 1, col];
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (ZeroChecker(col, input))
                                {
                                    col--;
                                }
                            }
                            if (AreBoardsSame())
                            {
                                goto input;
                            }
                            break;

                        case "up":
                            for (int col = 0; col <= board.GetUpperBound(1); col++)
                            {
                                for (int row = board.GetUpperBound(0); row > 0; row--)
                                {
                                    if (board[row - 1, col] == 0 || board[row - 1, col] == board[row, col])
                                    {
                                        board[row - 1, col] += board[row, col];
                                        if (row == board.GetUpperBound(0))
                                        {
                                            board[row, col] = 0;
                                        }
                                        else
                                        {
                                            board[row, col] = board[row - 1, col];
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }
                                if (ZeroChecker(col, input))
                                {
                                    col--;
                                }
                            }
                            if (AreBoardsSame())
                            {
                                goto input;
                            }
                            break;

                        default:
                            Console.WriteLine("Invalid input!");
                            goto input;
                    }
                } while (IsFull() == false || LastSpawnNeighbours());
            }
        }

        static bool LastSpawnNeighbours()
        {
            bool isThereEqualNeighbours = true;
            if (randomRow == 0)
            {
                if (randomCol == 0)
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol + 1] &&
                        board[randomRow, randomCol] != board[randomRow + 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
                else if (randomCol == board.GetUpperBound(1))
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol - 1] &&
                        board[randomRow, randomCol] != board[randomRow + 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
                else
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol - 1] &&
                        board[randomRow, randomCol] != board[randomRow, randomCol + 1] &&
                        board[randomRow, randomCol] != board[randomRow + 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
            }
            else if (randomRow == board.GetUpperBound(0))
            {
                if (randomCol == 0)
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol + 1] &&
                        board[randomRow, randomCol] != board[randomRow - 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
                else if (randomCol == board.GetUpperBound(1))
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol - 1] &&
                        board[randomRow, randomCol] != board[randomRow - 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
                else
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol - 1] &&
                        board[randomRow, randomCol] != board[randomRow, randomCol + 1] &&
                        board[randomRow, randomCol] != board[randomRow - 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
            }
            else
            {
                if (randomCol == 0)
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol + 1] &&
                        board[randomRow, randomCol] != board[randomRow - 1, randomCol] &&
                        board[randomRow, randomCol] != board[randomRow + 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
                else if (randomCol == board.GetUpperBound(1))
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol - 1] &&
                        board[randomRow, randomCol] != board[randomRow - 1, randomCol] &&
                        board[randomRow, randomCol] != board[randomRow + 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
                else
                {
                    if (board[randomRow, randomCol] != board[randomRow, randomCol - 1] &&
                        board[randomRow, randomCol] != board[randomRow, randomCol + 1] &&
                        board[randomRow, randomCol] != board[randomRow - 1, randomCol] &&
                        board[randomRow, randomCol] != board[randomRow + 1, randomCol])
                    {
                        isThereEqualNeighbours = false;
                    }
                }
            }
            return isThereEqualNeighbours;
        }

        static void Spawn()
        {
            Random rndm = new Random();   
            while (true)
            {
                randomRow = rndm.Next(0, board.GetUpperBound(0) + 1);
                randomCol = rndm.Next(0, board.GetUpperBound(1) + 1);
                if (board[randomRow, randomCol] == 0)
                {
                    board[randomRow, randomCol] = 2;
                    return;
                }
            }
        }

        static bool IsFull()
        {
            int counter = 0;
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                   if (board[row, col] != 0)
                   {
                       counter++;
                   }
                }
            }
            if (counter == 16)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool ZeroChecker(int rowOrCol, string input)
        {
            if (input == "right")
            {
                int row = rowOrCol;
                for (int col = 0; col < board.GetUpperBound(1); col++)
                {
                    if (board[row, col] != 0 && board[row, col + 1] == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            else if (input == "left")
            {
                int row = rowOrCol;
                for (int col = board.GetUpperBound(1); col > 0; col--)
                {
                    if (board[row, col] != 0 && board[row, col - 1] == 0)
                    {
                        return true;
                    }
                }
                return false;
            } 
            else if (input == "down")
            {
                int col = rowOrCol;
                for (int row = 0; row < board.GetUpperBound(0); row++)
                {
                    if (board[row, col] != 0 && board[row + 1, col] == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                int col = rowOrCol;
                for (int row = board.GetUpperBound(0); row > 0; row--)
                {
                    if (board[row, col] != 0 && board[row - 1, col] == 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        static void InitializeBoard()
        {
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                    board[row, col] = 0;
                }
            }
        }

        static bool AreBoardsSame()
        {
            bool result = true;
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                    if (tempBoard[row, col] != board[row, col])
                    {
                        result = false;
                    }
                }
            }
            return result;
        }

        static void CopyBoard()
        {
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                    tempBoard[row, col] = board[row, col];
                }
            }
        }

        static void PrintBoard()
        {
            for (int row = 0; row <= board.GetUpperBound(0); row++)
            {
                for (int col = 0; col <= board.GetUpperBound(1); col++)
                {
                    Console.Write("{0, 7}", board[row, col]);
                }
                Console.WriteLine();
            }
        }
    }
}