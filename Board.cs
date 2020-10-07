using System;
using System.Collections.Generic;
using System.IO;

namespace Puzzle
{
    public class Board
    {

        private System.Collections.Generic.List<List<BoardImp>> squareList;

        private Player mine;
        private Player target;
        private int x_row;
        private int x_col;
        private int cols;
        private int rows;
        private Square last;

        public Player Mine { get => mine; set => mine = value; }
        public Player Target { get => target; set => target = value; }
        public int X_row { get => x_row; set => x_row = value; }
        public int X_col { get => x_col; set => x_col = value; }
        public int Cols { get => cols; set => cols = value; }
        public int Rows { get => rows; set => rows = value; }
        public Square Last { get => last; set => last = value; }

        private void Load(String fileName)
        {

            squareList = new System.Collections.Generic.List<List<BoardImp>>();

            if (File.Exists(fileName))
            {

                // Open the file to read from.
                using (StreamReader sr = File.OpenText(fileName))
                {
                    string s;
                    int row = 0;
                    while ((s = sr.ReadLine()) != null)
                    {



                        List<BoardImp> rowList = new List<BoardImp>();
                        int size = s.Length;

                        System.Console.Out.Write("Line " + s + " size = " + size + "\n");


                        for (int count = 0; count < size; count++)
                        {
                            cols = size;
                            char chA = s[count];
                            System.Console.Out.Write("ch " + chA + "\n");
                            switch (chA)
                            {
                                case '#':
                                    Square q = new Square(count, row, SquareType.USED);
                                    rowList.Add(q);
                                    break;
                                case '0':
                                    q = new Square(count, row, SquareType.EMPTY);
                                    rowList.Add(q);
                                    break;
                                case 'F':
                                case 'f':
                                    target = new Player(count, row, PlayerType.TARGET);
                                    rowList.Add(target);
                                    break;
                                case 'X':
                                case 'x':
                                    mine = new Player(count, row, PlayerType.PLAYER);
                                    x_row = row;
                                    x_col = count;
                                    rowList.Add(mine);
                                    break;
                                default:
                                    throw new ArgumentException("Bad Board");
                            }
                        }
                        squareList.Add(rowList);

                        row++;



                    }
                    rows = row;
                }
            }
            for (int c = 0; c < rows; c++)
            {
                System.Console.Out.Write("dumping row " + c + "\n");
                List<BoardImp> list = squareList[c];
                for (int count = 0; count < list.Count; count++)
                {
                    BoardImp t = list[count];
                    System.Console.Out.Write("dumping " + t);
                }


            }

            System.Console.Out.Write("dumping player " + this.mine);
            System.Console.Out.Write("dumping Target " + this.target);

        }

        public int CanMoveLeft(int col, int row, bool notVisited)
        {
            int n = col - 1;
            if (n == -1)
            {
                return 0;
            }

            return IsEmptyAndNotVisited(n, row, notVisited);
        }

        public int CanMoveRight(int col, int row, bool notVisited)
        {
            int n = col + 1;
            if (n > cols - 1)
            {
                return 0;
            }

            return IsEmptyAndNotVisited(n, row, notVisited);
        }

        public int CanMoveUp(int col, int row, bool notVisited)
        {
            int n = row - 1;
            if (n == -1)
            {
                return 0;
            }

            return IsEmptyAndNotVisited(col, n, notVisited);
        }

        public int CanMoveDown(int col, int row, bool notVisited)
        {
            int n = row + 1;
            if (n > rows - 1)
            {
                return 0;
            }

            return IsEmptyAndNotVisited(col, n, notVisited);
        }


        public BoardImp GetSquare(int col, int row)
        {
            if (col >= 0 && col < cols
        && row >= 0 && row < rows)
            {
                List<BoardImp> ls = squareList[row];

                return ls[col];



            }
            return null;
        }


        public Square GetPiece(int col, int row)
        {

            System.Console.Out.Write("getPiece x = " + col + ",y =" + row + "\n");



            if (col >= 0 && col < cols
             && row >= 0 && row < rows)
            {
                List<BoardImp> ls = squareList[row];

                Object o = ls[col];
                if (o is Square c)
                {
                    return (Square)o;
                }


            }
            return null;
        }



        private int IsEmptyAndNotVisited(int col, int row, bool notVisited)
        {

            Square p = GetPiece(col, row);
            if (p == null)
            {

                BoardImp e = GetSquare(col, row);
                if (e is Player ps)
                {
                    if (ps.PlayerType == PlayerType.TARGET)
                    {
                        return 2;
                    }
                }

                return 0;
            }
            if (target.EqualsTo(col, row))
            {
                return 0;
            }

            bool empty = p.IsEmpty();
            bool visited = p.Visited;
            if (!notVisited && empty)
            {

                return 1;

            }
            else if (empty && !visited)
            {
                return 1;

            }

            return 0;
        }




        public void Move(Direction d)
        {
            switch (d)
            {
                case Direction.UP:
                    x_row--;
                    if (x_row == -1)
                    {
                        x_row = 0;
                    }
                    break;
                case Direction.DOWN:
                    x_row++;
                    if (x_row > rows - 1)
                    {
                        x_row = rows - 1;
                    }
                    break;
                case Direction.RIGHT:
                    x_col++;
                    if (x_col > cols - 1)
                    {
                        x_col = cols - 1;
                    }
                    break;
                case Direction.LEFT:
                    x_col--;
                    if (x_col == -1)
                    {
                        x_col = 0;
                    }
                    break;
                case Direction.NONE:
                    return;
            }

            Square u = this.GetPiece(x_col, x_row);
            u.Visited = true;
            last = u;
            System.Console.Out.Write("moving to " + u + " direction ==> " + d + "\n");

        }
        public Direction GetDirection(bool notVisited)
        {



            int status = CanMoveDown(x_col, x_row, notVisited);
            if (status == 1)
            {
                return Direction.DOWN;
            }
            else if ( status == 2)
            {
                return Direction.END;
            }

            status = CanMoveUp(x_col, x_row, notVisited);
            if (status == 1)
            {
                status = CanMoveLeft(x_col, x_row, notVisited);
                if (status == 1)
                {
                    return Direction.LEFT;
                }
                else if (status == 2)
                {
                    return Direction.END;
                }

                status = CanMoveRight(x_col, x_row, notVisited);
                if (status == 1)
                {
                    return Direction.RIGHT;
                }
                else if (status == 2)
                {
                    return Direction.END;
                }

                return Direction.UP;
            }
            status = CanMoveLeft(x_col, x_row, notVisited);

            if (status == 1)
            {
                return Direction.LEFT;
            }
            else if (status == 2)
            {
                return Direction.END;
            }
            status = CanMoveRight(x_col, x_row, notVisited);
            if (status == 1)
            {
                return Direction.RIGHT;
            }
            else if (status == 2)
            {
                return Direction.END;
            }

            return Direction.NONE;
        }

        public Board(string file)
        {
            try
            {
                Load(file);
            }
            catch (ArgumentException e)
            {
                System.Console.Out.Write(e.Message);
            }


        }
    }
}
