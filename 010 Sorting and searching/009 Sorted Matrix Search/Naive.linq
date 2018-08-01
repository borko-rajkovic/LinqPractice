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

    public static Boolean FindElement(int[][] matrix, int elem)
    {
        int row = 0;
        int col = matrix[0].Length - 1;
        while (row < matrix.Length && col >= 0)
        {
            if (matrix[row][col] == elem)
            {
                return true;
            }
            else if (matrix[row][col] > elem)
            {
                col--;
            }
            else
            {
                row++;
            }
        }
        return false;
    }

    public static void Main(String[] args)
    {
        int M = 10;
        int N = 5;
        int[][] matrix = new int[M][];
        for (int i=0; i<M; i++)
        {
            matrix[i] = new int[N];
        }
        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i][j] = 10 * i + j;
            }
        }

        PrintMatrix(matrix);

        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < M; j++)
            {
                int v = 10 * i + j;
                Console.WriteLine(v + ": " + FindElement(matrix, v));
            }
        }

    }
}
