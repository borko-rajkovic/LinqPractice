<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class SquareCell
    {
        public int zerosRight = 0;
        public int zerosBelow = 0;
        public SquareCell(int right, int below)
        {
            zerosRight = right;
            zerosBelow = below;
        }

        public void SetZerosRight(int right)
        {
            zerosRight = right;
        }

        public void SetZerosBelow(int below)
        {
            zerosBelow = below;
        }
    }

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

    public static Subsquare FindSquareWithSize(SquareCell[][] processed, int square_size)
    {
        // On an edge of length N, there are (N - sz + 1) squares of length sz.
        int count = processed.Length - square_size + 1;

        // Iterate through all squares with side length square_size.
        for (int row = 0; row < count; row++)
        {
            for (int col = 0; col < count; col++)
            {
                if (IsSquare(processed, row, col, square_size))
                {
                    return new Subsquare(row, col, square_size);
                }
            }
        }
        return null;
    }

    public static Subsquare FindSquare(int[][] matrix)
    {
        Debug.Assert(matrix.Length > 0);
        for (int row = 0; row < matrix.Length; row++)
        {
            Debug.Assert(matrix[row].Length == matrix.Length);
        }

        SquareCell[][] processed = ProcessSquare(matrix);

        int N = matrix.Length;

        for (int i = N; i >= 1; i--)
        {
            Subsquare square = FindSquareWithSize(processed, i);
            if (square != null)
            {
                return square;
            }
        }
        return null;
    }

    private static Boolean IsSquare(SquareCell[][] matrix, int row, int col, int size)
    {
        SquareCell topLeft = matrix[row][col];
        SquareCell topRight = matrix[row][col + size - 1];
        SquareCell bottomRight = matrix[row + size - 1][col];
        if (topLeft.zerosRight < size)
        { // Check top edge
            return false;
        }
        if (topLeft.zerosBelow < size)
        { // Check left edge
            return false;
        }
        if (topRight.zerosBelow < size)
        { // Check right edge
            return false;
        }
        if (bottomRight.zerosRight < size)
        { // Check bottom edge
            return false;
        }
        return true;
    }

    public static SquareCell[][] ProcessSquare(int[][] matrix)
    {
        SquareCell[][] processed = new SquareCell[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            processed[i] = new SquareCell[matrix.Length];
        }

        for (int r = matrix.Length - 1; r >= 0; r--)
        {
            for (int c = matrix.Length - 1; c >= 0; c--)
            {
                int rightZeros = 0;
                int belowZeros = 0;
                if (matrix[r][c] == 0)
                { // only need to process if it's a black cell
                    rightZeros++;
                    belowZeros++;
                    if (c + 1 < matrix.Length)
                    { // next column over is on same row
                        SquareCell previous = processed[r][c + 1];
                        rightZeros += previous.zerosRight;
                    }
                    if (r + 1 < matrix.Length)
                    {
                        SquareCell previous = processed[r + 1][c];
                        belowZeros += previous.zerosBelow;
                    }
                }
                processed[r][c] = new SquareCell(rightZeros, belowZeros);
            }
        }
        return processed;
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
