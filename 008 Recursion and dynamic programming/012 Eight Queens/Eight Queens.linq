<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int GRID_SIZE = 8;

    /* Check if (row1, column1) is a valid spot for a queen by checking if there
     * is a queen in the same column or diagonal. We don't need to check it for queens
     * in the same row because the calling placeQueen only attempts to place one queen at
     * a time. We know this row is empty. 
     */
    public static Boolean CheckValid(int[] columns, int row1, int column1)
    {
        for (int row2 = 0; row2 < row1; row2++)
        {
            int column2 = columns[row2];
            /* Check if (row2, column2) invalidates (row1, column1) as a queen spot. */

            /* Check if rows have a queen in the same column */
            if (column1 == column2)
            {
                return false;
            }

            /* Check diagonals: if the distance between the columns equals the distance
             * between the rows, then they’re in the same diagonal. */
            int columnDistance = Math.Abs(column2 - column1);
            int rowDistance = row1 - row2; // row1 > row2, so no need to use absolute value
            if (columnDistance == rowDistance)
            {
                return false;
            }
        }
        return true;
    }

    public static void PlaceQueens(int row, int[] columns, List<int[]> results)
    {
        if (row == GRID_SIZE)
        { // Found valid placement
            results.Add((int[])columns.Clone());
        }
        else
        {
            for (int col = 0; col < GRID_SIZE; col++)
            {
                if (CheckValid(columns, row, col))
                {
                    columns[row] = col; // Place queen
                    PlaceQueens(row + 1, columns, results);
                }
            }
        }
    }

    public static void Clear(int[] columns)
    {
        for (int i = 0; i < GRID_SIZE; i++)
        {
            columns[i] = -1;
        }
    }

    public static void PrintBoard(int[] columns)
    {
        DrawLine();
        for (int i = 0; i < GRID_SIZE; i++)
        {
            Console.Write("|");
            for (int j = 0; j < GRID_SIZE; j++)
            {
                if (columns[i] == j)
                {
                    Console.Write("Q|");
                }
                else
                {
                    Console.Write(" |");
                }
            }
            Console.Write("\n");
            DrawLine();
        }
        Console.WriteLine("");
    }

    private static void DrawLine()
    {
        StringBuilder line = new StringBuilder();
        for (int i = 0; i < GRID_SIZE * 2 + 1; i++)
            line.Append('-');
        Console.WriteLine(line.ToString());
    }

    public static void PrintBoards(List<int[]> boards)
    {
        for (int i = 0; i < boards.Count; i++)
        {
            int[] board = boards[i];
            PrintBoard(board);
        }
    }

    public static void Main(String[] args)
    {
        List<int[]> results = new List<int[]>();
        int[] columns = new int[GRID_SIZE];
        Clear(columns);
        PlaceQueens(0, columns, results);
        PrintBoards(results);
        Console.WriteLine(results.Count);
    }
}
