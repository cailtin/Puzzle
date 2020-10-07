using System;
using System.Text;
using System.Collections.Generic;

namespace Puzzle
{
    public class Square : BoardImp
    {
        private int col;
        private int row;
        private SquareType type;
        private Boolean visited;
        public Square(int x,int y,SquareType t)
        {
            this.col = x;
            this.row = y;
            Visited = false;
            this.type = t;

        }
        public bool IsEmpty() => type == SquareType.EMPTY;

        public override bool Equals(object obj)
        {
            return obj is Square square &&
                   col == square.col &&
                   row == square.row &&
                   type == square.type &&
                   visited == square.visited;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Row, Col, Visited, Type);
        }

        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }
        public bool Visited { get => visited; set => visited = value; }
        public SquareType Type { get => type; set => type = value; }

        public override string ToString()
        {
          StringBuilder buffer = new StringBuilder(300);
            buffer.Append("x=");
            buffer.Append(Col);
            buffer.Append(",y=");
            buffer.Append(Row);
            buffer.Append(",visited=");
            buffer.Append(Visited);
            buffer.Append(",type =");
            buffer.Append(Type);
            buffer.Append("\n");
            return buffer.ToString();



        }
    }
}
