<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

// If we are checking only last move
// (that is, prior to last move there is no winner)

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

    public static Piece HasWon(Piece[][] board, int row, int column)
    {
        if (board.Length != board[0].Length) return Piece.Empty;

        Piece piece = board[row][column];

        if (piece == Piece.Empty) return Piece.Empty;
        if (HasWonRow(board, row) || HasWonColumn(board, column))
        {
            return piece;
        }

        if (row == column && HasWonDiagonal(board, 1))
        {
            return piece;
        }

        if (row == (board.Length - column - 1) && HasWonDiagonal(board, -1))
        {
            return piece;
        }

        return Piece.Empty;
    }

    public static Boolean HasWonRow(Piece[][] board, int row)
    {
        for (int c = 1; c < board[row].Length; c++)
        {
            if (board[row][c] != board[row][0])
            {
                return false;
            }
        }
        return true;
    }

    public static Boolean HasWonColumn(Piece[][] board, int column)
    {
        for (int r = 1; r < board.Length; r++)
        {
            if (board[r][column] != board[0][column])
            {
                return false;
            }
        }
        return true;
    }

    public static Boolean HasWonDiagonal(Piece[][] board, int direction)
    {
        int row = 0;
        int column = direction == 1 ? 0 : board.Length - 1;
        Piece first = board[0][column];
        for (int i = 0; i < board.Length; i++)
        {
            if (board[row][column] != first)
            {
                return false;
            }
            row += 1;
            column += direction;
        }
        return true;
    }

	public static void Main(String[] args)
    {
        int N = 3;
        int[][] board_t = RandomMatrix(N, N, 0, 2);

        board_t[1][1] = board_t[0][2];
        board_t[2][0] = board_t[0][2];

        Piece[][] board = new Piece[N][];
        for (int i = 0; i < N; i ++)
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

        Piece p1 = HasWon(board, 0, 2);

        Console.WriteLine(p1);
        PrintMatrix(board_t);
    }
}
