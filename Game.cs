using System;
namespace Puzzle
{

    public class Game
    {
        private string fileName = "/home/clopez/projects/TestCanBeDeleted/src/testcanbedeleted/board.txt";

        private readonly Board board;

        public bool StartGame()
        {
            Direction lastMove = Direction.LEFT;
            int n = 0;
            bool found = false;
            bool run = true;
            while (run)
            {
                Direction d = board.GetDirection(true);
                if (d == Direction.END)
                {
                    System.Console.Out.Write("Found target");
                    return true;
                }
                if (d == Direction.NONE)
                {

                    if (board.X_row == 0) // I am a the top
                    {

                        switch (lastMove)
                        {
                            case Direction.LEFT:
                                // can move Right?
                                if (board.CanMoveRight(board.X_col, board.X_row, true) == 1)
                                {

                                    board.Move(Direction.RIGHT);
                                }
                                else
                                {
                                    throw new Exception("Invalid board");
                                }

                                break;
                            case Direction.RIGHT:
                                if (board.CanMoveLeft(board.X_col, board.X_row, true) == 1)
                                {

                                    board.Move(Direction.LEFT);
                                }
                                else
                                {
                                    throw new Exception("Invalid board");
                                }
                                break;
                            default:
                                if (board.CanMoveDown(board.X_col, board.X_row, false) == 1)
                                {
                                    board.Move(Direction.DOWN);
                                }
                                else
                                {
                                    throw new Exception("Invalid board");
                                }
                                break;
                        }
                    }
                    else if (board.X_row >= board.Rows - 1) // I am at the bottom
                    {
                        switch (lastMove)
                        {
                            case Direction.RIGHT:
                                n = board.X_col;
                                found = false;

                                for (int c = n; c > 0; c--)
                                {

                                    if (board.CanMoveUp(board.X_col, board.X_row, false) == 1)
                                    {
                                        board.Move(Direction.UP);

                                        found = true;
                                        break;
                                    }
                                    if (board.CanMoveLeft(board.X_col, board.X_row, false) == 1)
                                    {

                                        board.Move(Direction.LEFT);
                                    }
                                }
                                if (!found)
                                {
                                    throw new Exception("invalid board");
                                }
                                break;
                            case Direction.LEFT:

                                found = false;
                                n = board.X_col;
                                for (int c = n; c < board.Cols; c++)
                                {
                                    if (board.CanMoveUp(board.X_col, board.X_row, false) == 1)
                                    {
                                        board.Move(Direction.UP);

                                        found = true;
                                        break;
                                    }
                                    if (board.CanMoveRight(board.X_col, board.X_row, false) == 1)
                                    {

                                        board.Move(Direction.RIGHT);
                                    }
                                }

                                if (!found)
                                {
                                    throw new Exception("invalid board");
                                }
                                break;
                        }

                    }
                    else
                    {
                        /**
                         * get the last direction
                         *
                         * the idea is to move left and right finding a up/down
                         * position if none found abort else move to that new
                         * direction
                         *
                         */
                        Square last = board.Last;
                        found = false;
                        for (int c = last.Col; c < board.Cols; c++)
                        {

                            if (board.CanMoveRight(board.X_col, board.X_row, false) == 1)
                            {

                                board.Move(Direction.RIGHT);
                            }
                            if (board.CanMoveUp(board.X_col, board.X_row, true) == 1)
                            {
                                board.Move(Direction.UP);
                                found = true;
                                break;
                            }
                            if (board.CanMoveDown(board.X_col, board.X_row, true) == 1)
                            {
                                board.Move(Direction.DOWN);
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            for (int c = last.Col; c >= 0; c--)
                            {
                                if (board.CanMoveLeft(board.X_col, board.X_row, false) == 1)
                                {
                                    board.Move(Direction.LEFT);
                                }
                                if (board.CanMoveUp(board.X_col, board.X_row, true) == 1)
                                {
                                    board.Move(Direction.UP);
                                    found = true;
                                    break;
                                }
                                if (board.CanMoveDown(board.X_col, board.X_row, true) == 1)
                                {
                                    board.Move(Direction.DOWN);
                                    found = true;
                                    break;
                                }

                            }

                        }
                        if (!found)
                        {
                            throw new Exception("System error,unable to find a solution, last " + board.X_col + " " + board.X_row);
                        }

                    }
                    continue;
                }
                board.Move(d);

             
            }
            return false;
        }



        public Game()
        {
            try
            {
                board = new Board(fileName);
                StartGame();
            }
            catch (Exception e)
            {
                System.Console.Out.Write(e);
            }


        }
        static void Main(string[] args)
        {
            Game g = new Game();
        }
    }
    /*
 00000
 X#F#0
 0###0
 00000
 ##000

     */



}
