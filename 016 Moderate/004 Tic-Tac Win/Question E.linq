<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

// Solution for N X N matrix
// Using nested loops
// (repeating work and is not pretty)

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

    public static Piece HasWon(Piece[][] board)
    {
        int size = board.Length;
        if (board[0].Length != size) return Piece.Empty;
        Piece first;

        /* Check rows. */
        for (int i = 0; i < size; i++)
        {
            first = board[i][0];
            if (first == Piece.Empty) continue;
            for (int j = 1; j < size; j++)
            {
                if (board[i][j] != first)
                {
                    break;
                }
                else if (j == size - 1)
                {
                    return first;
                }
            }
        }

        /* Check columns. */
        for (int i = 0; i < size; i++)
        {
            first = board[0][i];
            if (first == Piece.Empty) continue;
            for (int j = 1; j < size; j++)
            {
                if (board[j][i] != first)
                {
                    break;
                }
                else if (j == size - 1)
                {
                    return first;
                }
            }
        }

        /* Check diagonals. */
        first = board[0][0];
        if (first != Piece.Empty)
        {
            for (int i = 1; i < size; i++)
            {
                if (board[i][i] != first)
                {
                    break;
                }
                else if (i == size - 1)
                {
                    return first;
                }
            }
        }

        first = board[0][size - 1];
        if (first != Piece.Empty)
        {
            for (int i = 1; i < size; i++)
            {
                if (board[i][size - i - 1] != first)
                {
                    break;
                }
                else if (i == size - 1)
                {
                    return first;
                }
            }
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
