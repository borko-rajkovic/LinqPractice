<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MaxMinutes(int[] massages)
    {
        return MaxMinutes(massages, 0);
    }

    public static int MaxMinutes(int[] massages, int index)
    {
        if (index >= massages.Length)
        { // Out of bounds
            return 0;
        }

        /* Best with this reservation. */
        int bestWith = massages[index] + MaxMinutes(massages, index + 2);

        /* Best without this reservation. */
        int bestWithout = MaxMinutes(massages, index + 1);

        /* Return best of this subarray, starting from index. */
        return Math.Max(bestWith, bestWithout);
    }

    public static void Main(String[] args)
    {
        int[] massages = { 30, 15, 60, 75, 45, 15, 15, 45 };
        Console.WriteLine(MaxMinutes(massages));
    }
}
