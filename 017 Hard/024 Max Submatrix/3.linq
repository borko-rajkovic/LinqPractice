<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Range
    {
        public int start, end, sum;
        public Range(int start, int end, int sum)
        {
            this.start = start;
            this.end = end;
            this.sum = sum;
        }
    }

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
        int colCount = matrix[0].Length;

        SubMatrix best = null;

        for (int rowStart = 0; rowStart < rowCount; rowStart++)
        {
            int[] partialSum = new int[colCount];

            for (int rowEnd = rowStart; rowEnd < rowCount; rowEnd++)
            {
                /* Add values at row rowEnd. */
                for (int i = 0; i < colCount; i++)
                {
                    partialSum[i] += matrix[rowEnd][i];
                }

                Range bestRange = MaxSubArray(partialSum, colCount);
                if (best == null || best.GetSum() < bestRange.sum)
                {
                    best = new SubMatrix(rowStart, bestRange.start, rowEnd, bestRange.end, bestRange.sum);
                }
            }
        }
        return best;
    }

    public static Range MaxSubArray(int[] array, int N)
    {
        Range best = null;
        int start = 0;
        int sum = 0;

        for (int i = 0; i < N; i++)
        {
            sum += array[i];
            if (best == null || sum > best.sum)
            {
                best = new Range(start, i, sum);
            }

            /* If running_sum is < 0 no point in trying to continue the 
             * series. Reset. */
            if (sum < 0)
            {
                start = i + 1;
                sum = 0;
            }
        }
        return best;
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
