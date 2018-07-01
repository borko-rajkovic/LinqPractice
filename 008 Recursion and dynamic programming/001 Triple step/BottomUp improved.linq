<Query Kind="Program" />

public static int CountWays(int n)
{
	if (n < 2) return 1;
	if (n == 2) return 2;
	
	int a = 1;
	int b = 1;
	int c = 2;	
	
	for (int i=3; i<=n; i++){
		int temp = a + b + c;
		a = b;
		b = c;
		c = temp;
	}
	
	return c;
}


public static void Main()
{
    for (int i = 0; i < 34; i++)
    {
        int ways = CountWays(i);
        Console.WriteLine(ways);
    }
}
