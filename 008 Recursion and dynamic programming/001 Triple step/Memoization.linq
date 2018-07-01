<Query Kind="Program" />

public static int CountWays(int n)
{
    int[] map = new int[n+1];
    return countWays(n, map);
}

public static int countWays(int n, int[] memo)
{
    if (n < 0)
    {
        return 0;
    }
    else if (n == 0)
    {
        return 1;
    }
    else if (memo[n] > 0)
    {
        return memo[n];
    }
    else
    {
        memo[n] = countWays(n - 1, memo) + countWays(n - 2, memo) + countWays(n - 3, memo);
        return memo[n];
    }
}


public static void Main()
{
    for (int i = 0; i < 34; i++)
    {
        int ways = CountWays(i);
        Console.WriteLine(ways);
    }
}
