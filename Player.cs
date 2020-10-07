using System;
using System.Text;

namespace Puzzle
{
    public class Player : BoardImp
    {
        private PlayerType playerType;
        private int row;
        private int col;
        public Player(int x, int y, PlayerType t)
        {
            Col = x;
            Row = y;
            PlayerType = t;
        }
        public Player() { }

        public PlayerType PlayerType { get => playerType; set => playerType = value; }
        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }

        public bool EqualsTo(int x, int y)
        {
            return x == Col && y == Row;
        }
        public override string ToString()
        {
            StringBuilder buffer = new StringBuilder(300);
            buffer.Append("x=");
            buffer.Append(Col);
            buffer.Append(",y=");
            buffer.Append(Row);

            buffer.Append(",type =");
            buffer.Append(playerType);
            buffer.Append("\n");
            return buffer.ToString();
        }
    }
}
