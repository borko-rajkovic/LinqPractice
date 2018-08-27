<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class SubMatrix
    {
        private int row1, row2, col1, col2, sum;
        public SubMatrix(int r1, int c1, int r2, int c2, int sm)
        {
            row1 = r1;
            col1 = c1;
            row2 = r2;
            col2 = c2;
            sum = sm;
        }

        public int GetSum()
        {
            return sum;
        }

        public override String ToString()
        {
            return "[(" + row1 + "," + col1 + ") -> (" + row2 + "," + col2 + ") = " + sum + "]";
        }
    }

    public static SubMatrix GetMaxMatrix(int[][] matrix)
    {
        SubMatrix best = null;
        int rowCount = matrix.Length;
        int columnCount = matrix[0].Length;
        int[][] sumThrough = PrecomputeSums(matrix);

        for (int row1 = 0; row1 < rowCount; row1++)
        {
            for (int row2 = row1; row2 < rowCount; row2++)
            {
                for (int col1 = 0; col1 < columnCount; col1++)
                {
                    for (int col2 = col1; col2 < columnCount; col2++)
                    {
                        int sum = Sum(sumThrough, row1, col1, row2, col2);
                        if (best == null || best.GetSum() < sum)
                        {
                            best = new SubMatrix(row1, col1, row2, col2, sum);
                        }
                    }
                }
            }
        }
        return best;
    }

    private static int[][] PrecomputeSums(int[][] matrix)
    {
        int[][] sumThrough = new int[matrix.Length][];
        for (int i = 0; i < matrix.Length; i++)
        {
            sumThrough[i] = new int[matrix[0].Length];
        }
        for (int r = 0; r < matrix.Length; r++)
        {
            for (int c = 0; c < matrix[0].Length; c++)
            {
                int left = c > 0 ? sumThrough[r][c - 1] : 0;
                int top = r > 0 ? sumThrough[r - 1][c] : 0;
                int overlap = r > 0 && c > 0 ? sumThrough[r - 1][c - 1] : 0;
                sumThrough[r][c] = left + top - overlap + matrix[r][c];
            }
        }
        return sumThrough;
    }

    private static int Sum(int[][] sumThrough, int r1, int c1, int r2, int c2)
    {
        int topAndLeft = r1 > 0 && c1 > 0 ? sumThrough[r1 - 1][c1 - 1] : 0;
        int left = c1 > 0 ? sumThrough[r2][c1 - 1] : 0;
        int top = r1 > 0 ? sumThrough[r1 - 1][c2] : 0;
        int full = sumThrough[r2][c2];
        return full - left - top + topAndLeft;
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
        int[][] matrix = RandomMatrix(10, 10, -5, 5);
        PrintMatrix(matrix);
        Console.WriteLine(GetMaxMatrix(matrix));
    }
}
