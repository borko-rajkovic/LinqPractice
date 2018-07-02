<Query Kind="Program" />

public class Program
{
    public static Boolean[][] RandomBooleanMatrix(int M, int N, int percentTrue)
    {
        Boolean[][] matrix = new Boolean[M][];
        for (int i = 0; i < N; i++)
        {
            matrix[i] = new bool[N];
        }

        for (int i = 0; i < M; i++)
        {
            for (int j = 0; j < N; j++)
            {
                matrix[i][j] = RandomBoolean(percentTrue);
            }
        }
        return matrix;
    }

    public static Boolean RandomBoolean(int percentTrue)
    {
        return RandomIntInRange(1, 100) <= percentTrue;
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return r.Next(n);
    }

    public static Random r = new Random();

    public static void PrintMatrix(Boolean[][] matrix)
    {
        for (int i = 0; i < matrix.Length; i++)
        {
            for (int j = 0; j < matrix[i].Length; j++)
            {
                if (matrix[i][j])
                {
                    Console.Write("1");
                }
                else
                {
                    Console.Write("0");
                }
            }
            Console.WriteLine();
        }
    }

    public class Point
    {
        public int row, column;
        public Point(int row, int column)
        {
            this.row = row;
            this.column = column;
        }

        public override String ToString()
        {
            return "(" + row + ", " + column + ")";
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override Boolean Equals(Object o)
        {
            if ((o is Point) && (((Point)o).row == row) && (((Point)o).column == column))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }



    public static List<Point> getPath(Boolean[][] maze)
    {
        if (maze == null || maze.Length == 0) return null;
        List<Point> path = new List<Point>();
        if (getPath(maze, maze.Length - 1, maze[0].Length - 1, path))
        {
            return path;
        }
        return null;
    }

    public static Boolean getPath(Boolean[][] maze, int row, int col, List<Point> path)
    {
        // If out of bounds or not available, return.
        if (col < 0 || row < 0 || !maze[row][col])
        {
            return false;
        }

        Boolean isAtOrigin = (row == 0) && (col == 0);

        // If there's a path from the start to my current location, add my location.
        if (isAtOrigin || getPath(maze, row, col - 1, path) || getPath(maze, row - 1, col, path))
        {
            Point p = new Point(row, col);
            path.Add(p);
            return true;
        }

        return false;
    }


    public static void Main(String[] args)
    {
        int size = 30;
        Boolean[][] maze = RandomBooleanMatrix(size, size, 70);

        PrintMatrix(maze);

        List<Point> path = getPath(maze);
        if (path != null)
        {
            foreach (var p in path)
            {
                Console.Write(p);
            }
        }
        else
        {
            Console.WriteLine("No path found.");
        }

    }
}
