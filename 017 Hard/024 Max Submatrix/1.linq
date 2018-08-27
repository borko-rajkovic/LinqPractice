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
        int rowCount = matrix.Length;
        int columnCount = matrix[0].Length;
        SubMatrix best = null;
        for (int row1 = 0; row1 < rowCount; row1++)
        {
            for (int row2 = row1; row2 < rowCount; row2++)
            {
                for (int col1 = 0; col1 < columnCount; col1++)
                {
                    for (int col2 = col1; col2 < columnCount; col2++)
                    {
                        int sum = Sum(matrix, row1, col1, row2, col2);
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

    private static int Sum(int[][] matrix, int row1, int col1, int row2, int col2)
    {
        int sum = 0;
        for (int r = row1; r <= row2; r++)
        {
            for (int c = col1; c <= col2; c++)
            {
                sum += matrix[r][c];
            }
        }
        return sum;
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
