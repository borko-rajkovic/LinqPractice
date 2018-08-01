<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Coordinate : ICloneable
    {
        public int row;
        public int column;
        public Coordinate(int r, int c)
        {
            row = r;
            column = c;
        }

        public Boolean Inbounds(int[][] matrix)
        {
            return row >= 0 &&
                    column >= 0 &&
                    row < matrix.Length &&
                    column < matrix[0].Length;
        }

        public Boolean IsBefore(Coordinate p)
        {
            return row <= p.row && column <= p.column;
        }

        public void MoveDownRight()
        {
            row++;
            column++;
        }

        public void SetToAverage(Coordinate min, Coordinate max)
        {
            row = (min.row + max.row) / 2;
            column = (min.column + max.column) / 2;
        }

        public object Clone()
        {
            return new Coordinate(row, column);
        }
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

    public static Coordinate PartitionAndSearch(int[][] matrix, Coordinate origin, Coordinate dest, Coordinate pivot, int x)
    {
        Coordinate lowerLeftOrigin = new Coordinate(pivot.row, origin.column);
        Coordinate lowerLeftDest = new Coordinate(dest.row, pivot.column - 1);
        Coordinate upperRightOrigin = new Coordinate(origin.row, pivot.column);
        Coordinate upperRightDest = new Coordinate(pivot.row - 1, dest.column);

        Coordinate lowerLeft = FindElement(matrix, lowerLeftOrigin, lowerLeftDest, x);
        if (lowerLeft == null)
        {
            return FindElement(matrix, upperRightOrigin, upperRightDest, x);
        }
        return lowerLeft;
    }

    public static Coordinate FindElement(int[][] matrix, Coordinate origin, Coordinate dest, int x)
    {
        if (!origin.Inbounds(matrix) || !dest.Inbounds(matrix))
        {
            return null;
        }
        if (matrix[origin.row][origin.column] == x)
        {
            return origin;
        }
        else if (!origin.IsBefore(dest))
        {
            return null;
        }

        /* Set start to start of diagonal and end to the end of the diagonal. Since
         * the grid may not be square, the end of the diagonal may not equal dest.
         */
        Coordinate start = (Coordinate)origin.Clone();
        int diagDist = Math.Min(dest.row - origin.row, dest.column - origin.column);
        Coordinate end = new Coordinate(start.row + diagDist, start.column + diagDist);
        Coordinate p = new Coordinate(0, 0);

        /* Do binary search on the diagonal, looking for the first element greater than x */
        while (start.IsBefore(end))
        {
            p.SetToAverage(start, end);
            if (x > matrix[p.row][p.column])
            {
                start.row = p.row + 1;
                start.column = p.column + 1;
            }
            else
            {
                end.row = p.row - 1;
                end.column = p.column - 1;
            }
        }

        /* Split the grid into quadrants. Search the bottom left and the top right. */
        return PartitionAndSearch(matrix, origin, dest, start, x);
    }

    public static Coordinate FindElement(int[][] matrix, int x)
    {
        Coordinate origin = new Coordinate(0, 0);
        Coordinate dest = new Coordinate(matrix.Length - 1, matrix[0].Length - 1);
        return FindElement(matrix, origin, dest, x);
    }

    public static void Main(String[] args)
    {
        int[][] matrix = {  new int[] {15, 30,  50,  70,  73},
                            new int[] {35, 40, 100, 102, 120},
                            new int[] {36, 42, 105, 110, 125},
                            new int[] {46, 51, 106, 111, 130},
                            new int[] {48, 55, 109, 140, 150}};

        PrintMatrix(matrix);
        int m = matrix.Length;
        int n = matrix[0].Length;

        int count = 0;
        int littleOverTheMax = matrix[m - 1][n - 1] + 10;
        for (int i = 0; i < littleOverTheMax; i++)
        {
            Coordinate c = FindElement(matrix, i);
            if (c != null)
            {
                Console.WriteLine(i + ": (" + c.row + ", " + c.column + ")");
                count++;
            }
        }
        Console.WriteLine("Found " + count + " unique elements.");
    }
}
