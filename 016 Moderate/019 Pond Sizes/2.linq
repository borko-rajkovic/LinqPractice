<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<int> ComputePondSizes(int[][] land)
    {
        Boolean[][] visited = new Boolean[land.Length][];
        for (int i = 0; i < land.Length; i++)
        {
            visited[i] = new Boolean[land[0].Length];
        }            
        List<int> pondSizes = new List<int>();
        for (int r = 0; r < land.Length; r++)
        {
            for (int c = 0; c < land[r].Length; c++)
            {
                int size = ComputeSize(land, visited, r, c);
                if (size > 0)
                {
                    pondSizes.Add(size);
                }
            }
        }
        return pondSizes;
    }

    public static int ComputeSize(int[][] land, Boolean[][] visited, int row, int col)
    {
        /* If out of bounds or already visited. */
        if (row < 0 || col < 0 || row >= land.Length || col >= land[row].Length || visited[row][col] || land[row][col] != 0)
        {
            return 0;
        }
        int size = 1;
        visited[row][col] = true;
        for (int dr = -1; dr <= 1; dr++)
        {
            for (int dc = -1; dc <= 1; dc++)
            {
                size += ComputeSize(land, visited, row + dr, col + dc);
            }
        }
        return size;
    }

    public static void Main(String[] args)
    {
        int[][] land = new int[][] {
            new int[] { 0, 2, 1, 0 },
            new int[] { 0, 1, 0, 1 },
            new int[] { 1, 1, 0, 1 },
            new int[] { 0, 1, 0, 1 } };
        List<int> sizes = ComputePondSizes(land);
        foreach (int sz in sizes)
        {
            Console.WriteLine(sz);
        }
    }
}
