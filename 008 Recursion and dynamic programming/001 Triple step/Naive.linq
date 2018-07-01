<Query Kind="Program" />

public static int CountWays(int n)
{
    if (n < 0)
    {
        return 0;
    }
    else if (n == 0)
    {
        return 1;
    }
    else
    {
        return CountWays(n - 1) + CountWays(n - 2) + CountWays(n - 3);
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
