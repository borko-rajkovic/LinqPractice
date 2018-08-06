<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MakeChange(int amount, int[] denoms, int index, int[][] map)
    {
        if (map[amount][index] > 0)
        { // retrieve value
            return map[amount][index];
        }
        if (index >= denoms.Length - 1) return 1; // one denom remaining -> one way to do it
        int denomAmount = denoms[index];
        int ways = 0;
        for (int i = 0; i * denomAmount <= amount; i++)
        {
            // go to next denom, assuming i coins of denomAmount
            int amountRemaining = amount - i * denomAmount;
            ways += MakeChange(amountRemaining, denoms, index + 1, map);
        }
        map[amount][index] = ways;
        return ways;
    }

    public static int MakeChange(int n, int[] denoms)
    {
        int[][] map = new int[n + 1][];
        for (int i = 0; i < n + 1; i++)
        {
            map[i] = new int[denoms.Length];
        }
        return MakeChange(n, denoms, 0, map);
    }

    public static void Main(String[] args)
    {
        int[] denoms = { 25, 10, 5, 1 };
        int ways = MakeChange(24000, denoms);
        Console.WriteLine(ways);
    }
}
