<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

// populate hash table with all possible combinations
//
// next, convert board to int using 3-digit system:
// 3^0 * V(0) + 3^1 * V(1) + 3^2 * V(2) + ... + 3^8 * V(8)
// where V(i) is 0, 1 or 2

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