<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MaxMinutes(int[] massages)
    {
        int oneAway = 0;
        int twoAway = 0;
        for (int i = massages.Length - 1; i >= 0; i--)
        {
            int bestWith = massages[i] + twoAway;
            int bestWithout = oneAway;
            int current = Math.Max(bestWith, bestWithout);
            twoAway = oneAway;
            oneAway = current;
        }
        return oneAway;
    }

    public static void Main(String[] args)
    {
        int[] massages = { 2 * 15, 1 * 15, 4 * 15, 5 * 15, 3 * 15, 1 * 15, 1 * 15, 3 * 15 };
        Console.WriteLine(MaxMinutes(massages));
    }
}
