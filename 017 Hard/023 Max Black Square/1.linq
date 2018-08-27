<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Subsquare
    {
        public int row, column, size;
        public Subsquare(int r, int c, int sz)
        {
            row = r;
            column = c;
            size = sz;
        }

        public void Print()
        {
            Console.WriteLine("(" + row + ", " + column + ", " + size + ")");
        }
    }

    public static Subsquare FindSquareWithSize(int[][] matrix, int squareSize)
    {
        // On an edge of length N, there are (N - sz + 1) squares of length sz.
        int count = matrix.Length - squareSize + 1;

        // Iterate through all squares with side length square_size.
        for (int row = 0; row < count; row++)
        {
            for (int col = 0; col < count; col++)
            {
                if (IsSquare(matrix, row, col, squareSize))
                {
                    return new Subsquare(row, col, squareSize);
                }
            }
        }
        return null;
    }

    public static Subsquare FindSquare(int[][] matrix)
    {
        Debug.Assert(matrix.Length > 0);
        Debug.Assert(matrix.Length > 0);
        for (int row = 0; row < matrix.Length; row++)
        {
            Debug.Assert(matrix[row].Length == matrix.Length);
            Debug.Assert(matrix[row].Length == matrix.Length);
        }

        int N = matrix.Length;

        for (int i = N; i >= 1; i--)
        {
            Subsquare square = FindSquareWithSize(matrix, i);
            if (square != null)
            {
                return square;
            }
        }
        return null;
    }

    private static Boolean IsSquare(int[][] matrix, int row, int col, int size)
    {
        // Check top and bottom border.
        for (int j = 0; j < size; j++)
        {
            if (matrix[row][col + j] == 1)
            {
                return false;
            }
            if (matrix[row + size - 1][col + j] == 1)
            {
                return false;
            }
        }

        // Check left and right border.
        for (int i = 1; i < size - 1; i++)
        {
            if (matrix[row + i][col] == 1)
            {
                return false;
            }
            if (matrix[row + i][col + size - 1] == 1)
            {
                return false;
            }
        }
        return true;
    }

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

    private static readonly Random RandomIntNumbers = new Random();

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int[][] RandomMatrix(int m, int n, int min, int max)
    {
        int[][] matrix = new int[m][];
        for (int i = 0; i < m; i++)
        {
            matrix[i] = new int[n];
            for (int j = 0; j < n; j++)
            {
                matrix[i][j] = RandomIntInRange(min, max);
            }
        }
        return matrix;
    }

    public static void Main(String[] args)
    {
        int[][] matrix = RandomMatrix(7, 7, 0, 1);
        PrintMatrix(matrix);
        Subsquare square = FindSquare(matrix);
        square.Print();
    }
}
