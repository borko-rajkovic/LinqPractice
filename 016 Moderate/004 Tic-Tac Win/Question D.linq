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

    public static int[][] RandomMatrix(int M, int N, int min, int max)
    {
        int[][] matrix = new int[M][];
        for (int i = 0; i < M; i++)
        {
            matrix[i] = new int[N];
        }
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i][j] = RandomIntInRange(min, max);
            }
        }
        return matrix;
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static void PrintMatrix(int[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j] < 10 && matrix[i][j] > -10)
                {
                    Console.Write(" ");
                }
                if (matrix[i][j] < 100 && matrix[i][j] > -100)
                {
                    Console.Write(" ");
                }
                if (matrix[i][j] >= 0)
                {
                    Console.Write(" ");
                }
                Console.Write(" " + matrix[i][j]);
            }
            Console.WriteLine();
        }
    }

    public static Boolean HasWinner(Piece p1, Piece p2, Piece p3)
    {
        if (p1 == Piece.Empty)
        {
            return false;
        }
        return p1 == p2 && p2 == p3;
    }

    public static Piece HasWon(Piece[][] board)
    {
        if (board[0][0] != Piece.Empty &&
            (HasWinner(board[0][0], board[0][1], board[0][2]) ||
             HasWinner(board[0][0], board[1][0], board[2][0])))
        {
            return board[0][0];
        }

        if (board[2][2] != Piece.Empty &&
            (HasWinner(board[2][0], board[2][1], board[2][2]) ||
             HasWinner(board[0][2], board[1][2], board[2][2])))
        {
            return board[2][2];
        }

        if (board[1][1] != Piece.Empty &&
            (HasWinner(board[0][0], board[1][1], board[2][2]) ||
             HasWinner(board[0][2], board[1][1], board[2][0]) ||
             HasWinner(board[1][0], board[1][1], board[1][2]) ||
             HasWinner(board[0][1], board[1][1], board[2][1])))
        {
            return board[1][1];
        }

        return Piece.Empty;
    }

    public static void Main(String[] args)
    {

        int N = 3;
        int[][] board_t = RandomMatrix(N, N, 0, 2);
        Piece[][] board = new Piece[N][];
        for (int i = 0; i < N; i++)
        {
            board[i] = new Piece[N];
        }

        for (int i = 0; i < N; i++)
        {
            for (int j = 0; j < N; j++)
            {
                int x = board_t[i][j];
                board[i][j] = (Piece)x;
            }
        }

        Piece p1 = HasWon(board);

        Console.WriteLine(p1);
        PrintMatrix(board_t);
    }
}
