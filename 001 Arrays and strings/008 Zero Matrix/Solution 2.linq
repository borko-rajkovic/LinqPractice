<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
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

    private static readonly Random RandomIntNumbers = new Random();

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static void NullifyRow(int[][] matrix, int row)
    {
        for (int j = 0; j < matrix[0].Length; j++)
        {
            matrix[row][j] = 0;
        }
    }

    public static void NullifyColumn(int[][] matrix, int col)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            matrix[i][col] = 0;
        }
    }

    public static void SetZeros(int[][] matrix)
    {
        Boolean rowHasZero = false;
        Boolean colHasZero = false;

        // Check if first row has a zero
        for (int j = 0; j < matrix[0].Length; j++)
        {
            if (matrix[0][j] == 0)
            {
                rowHasZero = true;
                break;
            }
        }

        // Check if first column has a zero
        for (int i = 0; i < matrix.Length; i++)
        {
            if (matrix[i][0] == 0)
            {
                colHasZero = true;
                break;
            }
        }

        // Check for zeros in the rest of the array
        for (int i = 1; i < matrix.Length; i++)
        {
            for (int j = 1; j < matrix[0].Length; j++)
            {
                if (matrix[i][j] == 0)
                {
                    matrix[i][0] = 0;
                    matrix[0][j] = 0;
                }
            }
        }

        // Nullify rows based on values in first column
        for (int i = 1; i < matrix.Length; i++)
        {
            if (matrix[i][0] == 0)
            {
                NullifyRow(matrix, i);
            }
        }

        // Nullify columns based on values in first row
        for (int j = 1; j < matrix[0].Length; j++)
        {
            if (matrix[0][j] == 0)
            {
                NullifyColumn(matrix, j);
            }
        }

        // Nullify first row
        if (rowHasZero)
        {
            NullifyRow(matrix, 0);
        }

        // Nullify first column
        if (colHasZero)
        {
            NullifyColumn(matrix, 0);
        }
    }

    public static Boolean MatricesAreEqual(int[][] m1, int[][] m2)
    {
        if (m1.Length != m2.Length || m1[0].Length != m2[0].Length)
        {
            return false;
        }

        for (int k = 0; k < m1.Length; k++)
        {
            for (int j = 0; j < m1[0].Length; j++)
            {
                if (m1[k][j] != m2[k][j])
                {
                    return false;
                }
            }
        }
        return true;
    }

    public static void Main(String[] args)
    {
        int nrows = 10;
        int ncols = 15;
        int[][] matrix = RandomMatrix(nrows, ncols, -10, 10);

        PrintMatrix(matrix);

        SetZeros(matrix);

        Console.WriteLine();

        PrintMatrix(matrix);

    }

}
