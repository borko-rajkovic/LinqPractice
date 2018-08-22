<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MaxMinutes(int[] massages)
    {
        /* Allocating two extra slots in the array so we don't have to do bounds
           * checking on lines 7 and 8. */
        int[] memo = new int[massages.Length + 2];
        memo[massages.Length] = 0;
        memo[massages.Length + 1] = 0;
        for (int i = massages.Length - 1; i >= 0; i--)
        {
            int bestWith = massages[i] + memo[i + 2];
            int bestWithout = memo[i + 1];
            memo[i] = Math.Max(bestWith, bestWithout);
        }
        return memo[0];
    }

    public static void Main(String[] args)
    {
        int[] massages = { 2 * 15, 1 * 15, 4 * 15, 5 * 15, 3 * 15, 1 * 15, 1 * 15, 3 * 15 };
        Console.WriteLine(MaxMinutes(massages));
    }
}
