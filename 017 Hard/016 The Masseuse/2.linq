<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MaxMinutes(int[] massages)
    {
        int[] memo = new int[massages.Length];
        return MaxMinutes(massages, 0, memo);
    }

    public static int MaxMinutes(int[] massages, int index, int[] memo)
    {
        if (index >= massages.Length)
        {
            return 0;
        }
        if (memo[index] == 0)
        {
            int bestWith = massages[index] + MaxMinutes(massages, index + 2, memo);
            int bestWithout = MaxMinutes(massages, index + 1, memo);
            memo[index] = Math.Max(bestWith, bestWithout);
        }

        return memo[index];
    }

    public static void Main(String[] args)
    {
        int[] massages = { 2 * 15, 1 * 15, 4 * 15, 5 * 15, 3 * 15, 1 * 15, 1 * 15, 3 * 15 };
        Console.WriteLine(MaxMinutes(massages));
    }
}
