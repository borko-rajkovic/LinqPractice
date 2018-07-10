<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public enum Piece { Empty, Red, Blue };

    public class Position
    {
        public int row, column;
        public Position(int row, int column)
        {
            this.row = row;
            this.column = column;
        }
    }

    public static int ConvertBoardToInt(Piece[][] board)
    {
        int sum = 0;
        for (int i = 0; i < board.Length; i++)
        {
            for (int j = 0; j < board[i].Length; j++)
            {
                int value = (int)board[i][j];
                sum = sum * 3 + value;
            }
        }
        return sum;
    }

    public static void Main(String[] args)
    {
        Piece[][] board = {
            new Piece[] {Piece.Empty, Piece.Empty, Piece.Empty},
            new Piece[] {Piece.Empty, Piece.Empty, Piece.Empty},
            new Piece[] {Piece.Blue, Piece.Blue, Piece.Blue}};

        int v = ConvertBoardToInt(board);
        Console.WriteLine(v);
    }
}
