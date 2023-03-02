using System;
using System.Threading;

namespace Pbl_3
{
    class Program
    {
        //{pieceindex,column,row} {6,28,16} {5,24,16},{13,24,14}
        // static int[,] computerNumber = {{0,3,2}, {1,5,2}, {2,7,2}, {3,3,3}, {4,5,3}, {5,7,3}, {6,3,4}, {7,5,4},{8,7,4}};

        // {3,16,2}
        // static int[,] humanNumber = {{0,17,9}, {1,15,9}, {2,13,9}, {3,17,8}, {4,15,8}, {5,13,8}, {6,17,7}, {7,15,7},{8,13,7}};
        static char[,] board = {{'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'},
                                {'.','.','.','.','.','.','.','.'}};
        static int cursorx = 3;
        static int cursory = 2;
        static int tourCounter = 0;
        static int[] pieceFirst = new int[3];
        static bool flag = false;
        static bool movement = false;
        static bool jump1 = false;
        static bool jump2 = false;
        static int compcheck = 0;
        static void Main(string[] args)
        {

            DisplayBoard();
            CursorMovement();
        }
        static void DisplayBoard()
        {
            DisplayPieces();
            Console.SetCursorPosition(0, 0);



            char[] numbers = { '1', '2', '3', '4', '5', '6', '7', '8' };
            for (int i = 0; i < 10; i++)
            {
                if (i % 9 == 0)
                {
                    if (i == 0)
                    {
                        Console.WriteLine("   1 2 3 4 5 6 7 8");
                    }

                    Console.WriteLine(" + - - - - - - - - +");
                }
                else
                {
                    Console.Write(i + "|                 |" + "\n");
                }
            }
            Console.SetCursorPosition(3, 4);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                Console.SetCursorPosition(3, i + 2);
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }

        }
        static void DisplayPieces()
        {
            if (flag == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        board[i, j] = 'O';
                    }
                }
                for (int i = 5; i < 8; i++)
                {
                    for (int j = 5; j < 8; j++)
                    {
                        board[i, j] = 'X';
                    }
                }
                flag = true;
            }

        }
        static void CursorMovement()
        {
            Console.SetCursorPosition(cursorx, cursory);
            while (true)
            {
                ConsoleKeyInfo cki;
                if (Console.KeyAvailable)
                {
                    cki = Console.ReadKey(true);
                    Console.SetCursorPosition(22, 3);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Round:" + tourCounter);
                    Console.SetCursorPosition(22, 5);

                    if (tourCounter % 2 == 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Turn:X-O");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Turn:O");

                    }
                    Console.ResetColor();
                    if (GameOver('h',0)==true)
                    {
                        Console.SetCursorPosition(22, 7);
                        Console.WriteLine("Winner: X");
                    }
                    if (GameOver('c', 0) == true)
                    {
                        Console.SetCursorPosition(22, 7);
                        Console.WriteLine("Winner: O");
                    }

                    if (cki.Key == ConsoleKey.RightArrow && cursorx < 17)
                    {

                        cursorx += 2;
                        Console.SetCursorPosition(cursorx, cursory);
                    }

                    if (cki.Key == ConsoleKey.LeftArrow && cursorx > 3)
                    {
                        cursorx -= 2;
                        Console.SetCursorPosition(cursorx, cursory);
                    }

                    if (cki.Key == ConsoleKey.UpArrow && cursory > 2)
                    {
                        cursory -= 1;
                        Console.SetCursorPosition(cursorx, cursory);
                    }


                    if (cki.Key == ConsoleKey.DownArrow && cursory < 9)
                    {
                        cursory += 1;
                        Console.SetCursorPosition(cursorx, cursory);
                    }
                    if (cki.Key == ConsoleKey.Z && tourCounter % 2 == 0)
                    {
                        if (tourCounter % 2 == 0)
                        {
                            pieceFirst[1] = cursorx;
                            pieceFirst[2] = cursory;
                            flag = true;
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                    }
                    if ((cki.Key == ConsoleKey.X && tourCounter % 2 == 0) && MovabilityControl('h', pieceFirst) == true)
                    {

                        board[cursory - 2, (cursorx / 2) - 1] = board[pieceFirst[2] - 2, (pieceFirst[1] / 2) - 1];
                        board[pieceFirst[2] - 2, (pieceFirst[1] / 2) - 1] = '.';

                        DisplayBoard();
                        DisplayPieces();
                        Console.SetCursorPosition(cursorx, cursory);
                        //movement = false;
                        tourCounter++;
                    }
                    if ((cki.Key == ConsoleKey.C))
                    {
                        movement = false;
                        while (movement == false && MovabilityControl('o', pieceFirst) == true)
                        {
                            DisplayBoard();
                            DisplayPieces();
                            Console.SetCursorPosition(cursorx, cursory);
                        }
                        tourCounter++;


                    }
                }
            }
            static bool MovabilityControl(char side, int[] pieceFirst)
            {
                if (side == 'h')
                {

                    // sürükleme olacaksa
                    if (board[pieceFirst[2] - 2, (pieceFirst[1] / 2) - 1] == 'X' && board[cursory - 2, (cursorx / 2) - 1] == '.' && (cursory - pieceFirst[2] == -1 || cursorx - pieceFirst[1] == -2)) return true;

                    // atlama olacaksa

                    else if ((board[pieceFirst[2] - 2, (pieceFirst[1] / 2) - 1] == 'X' && board[cursory - 2, (cursorx / 2) - 1] == '.') &&
                        (board[(cursory + pieceFirst[2]) / 2 - 2, (cursorx / 2) - 1] != '.' || board[cursory - 2, (((pieceFirst[1] / 2) - 1) + ((cursorx / 2) - 1)) / 2] != '.'))
                    {
                        return true;
                    }
                    else if ((board[pieceFirst[2] - 2, (pieceFirst[1] / 2) - 1] == 'X' && board[cursory - 2, (cursorx / 2) - 1] == '.') &&
                        ((cursory - pieceFirst[2]) % 1 == 0 || (cursorx - pieceFirst[1]) % 2 == 0))
                    {
                        return true;
                    }


                }
                else if (side == 'o')
                {
                    Random rnd = new Random();
                    int r = rnd.Next(2);
                    compcheck = 0;

                    for (int i = 0; i < board.GetLength(0) - 2; i++)
                    {
                        for (int j = 0; j < board.GetLength(1) - 2; j++)
                        {
                            if (board[i, j] == 'O')
                            {
                                if (compcheck == 0 && (board[i + 1, j] != '.' && board[i + 2, j] == '.'))
                                {
                                    if (board[i + 2, j + 1] != '.' && board[i + 2, j + 2] == '.')
                                    {
                                        board[i + 2, j + 2] = board[i, j];
                                        board[i, j] = '.';
                                        compcheck = 3;
                                        movement = true;
                                        return true;
                                    }
                                    if (i < 5 && (board[i + 3, j] != '.' && board[i + 4, j] == '.') && compcheck == 0)
                                    {
                                        board[i + 4, j] = board[i, j];
                                        board[i, j] = '.';
                                        compcheck = 3;
                                        movement = true;
                                        return true;
                                    }
                                }
                                if (compcheck == 0 && (board[i, j + 1] != '.' && board[i, j + 2] == '.'))
                                {
                                    if (board[i + 1, j + 2] != '.' && board[i + 2, j + 2] == '.')
                                    {
                                        board[i + 2, j + 2] = board[i, j];
                                        board[i, j] = '.';
                                        compcheck = 3;
                                        movement = true;
                                        return true;
                                    }
                                    if (j < 5 && (board[i, j + 3] != '.' && board[i, j + 4] == '.') && compcheck == 0)
                                    {
                                        board[i, j + 4] = board[i, j];
                                        board[i, j] = '.';
                                        compcheck = 3;
                                        movement = true;
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 0; i < board.GetLength(0) - 2; i++)
                    {
                        for (int j = 0; j < board.GetLength(1) - 2; j++)
                        {
                            if (board[i, j] == 'O')
                            {
                                if (((board[i + 1, j] != '.' && board[i + 2, j] == '.')) && compcheck < 2 && r == 0)
                                {
                                    board[i + 2, j] = board[i, j];
                                    board[i, j] = '.';
                                    compcheck = 2;
                                    movement = true;
                                    return true;
                                }
                                if ((board[i, j + 1] != '.' && board[i, j + 2] == '.') && compcheck < 2 && r == 1)
                                {
                                    board[i, j + 2] = board[i, j];
                                    board[i, j] = '.';
                                    compcheck = 2;
                                    movement = true;
                                    return true;
                                }
                            }


                        }
                    }

                    for (int i = 0; i < board.GetLength(0) ; i++)
                    {
                        for (int j = 0; j < board.GetLength(1) ; j++)
                        {
                            if (board[i, j] == 'O')
                            {
                                if (r == 0 && compcheck == 0 && (board[i + 1, j] == '.')&&i!=7)
                                {
                                    board[i + 1, j] = board[i, j];
                                    board[i, j] = '.';
                                    compcheck = 1;
                                    movement = true;
                                    return true;
                                }
                                if (j!=7&&r == 1 && board[i, j + 1] == '.' && compcheck == 0)
                                {
                                    board[i, j + 1] = board[i, j];
                                    board[i, j] = '.';
                                    compcheck = 1;
                                    movement = true;
                                    return true;
                                }
                            }
                        }
                    }


                }
                else
                {
                    return false;
                }
                return false;
            }
            static bool GameOver(char s, int counter)
            {
                if (s == 'h')
                {
                    counter = 0;
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == 'X')
                            {
                                counter++;
                            }
                        }
                    }
                }
                if (s == 'c')
                {
                    for (int i = 5; i < 8; i++)
                    {
                        for (int j = 5; j < 8; j++)
                        {
                            if (board[i, j] == 'O')
                            {
                                counter++;
                            }
                        }
                    }
                }
                if (s == 'h' && counter == 9)
                {
                    return true;
                }
                if (s == 'c' && counter == 9)
                {
                    
                    return true;
                }
                return false;
            }


        }
    }
}


