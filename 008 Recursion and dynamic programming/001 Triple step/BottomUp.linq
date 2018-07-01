<Query Kind="Program" />

public static int CountWays(int n)
{
    int[] memo = new int[Math.Max(3, n+1)];
    
	memo[0] = 1;
	memo[1] = 1;
	memo[2] = 2;
	
	for (int i=3; i<=n; i++){
	  memo[i] = memo[i-1]+memo[i-2]+memo[i-3];
	}
	return memo[n];
}


public static void Main()
{
    for (int i = 0; i < 34; i++)
    {
        int ways = CountWays(i);
        Console.WriteLine(ways);
    }
}
