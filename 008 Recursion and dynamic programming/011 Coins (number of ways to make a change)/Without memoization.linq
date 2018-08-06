<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MakeChange(int amount, int[] denoms, int index)
    {
        if (index >= denoms.Length - 1) return 1; // one denom remaining -> one way to do it
        int denomAmount = denoms[index];
        int ways = 0;
        for (int i = 0; i * denomAmount <= amount; i++)
        {
            int amountRemaining = amount - i * denomAmount;
            ways += MakeChange(amountRemaining, denoms, index + 1); // go to next denom
        }
        return ways;
    }

    public static int MakeChange(int amount, int[] denoms)
    {
        return MakeChange(amount, denoms, 0);
    }

    public static void Main(String[] args)
    {
        int[] denoms = { 25, 10, 5, 1 };
        int ways = MakeChange(24000, denoms);
        Console.WriteLine(ways);
    }
}
