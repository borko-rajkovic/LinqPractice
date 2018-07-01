<Query Kind="Program" />


	public static int Fibonacci(int n) {
		return Fibonacci(n, new int[n + 1]);
	}
	
	public static int Fibonacci(int i, int[] memo) {
		if (i == 0) return 0;
		else if (i == 1) return 1;
		
		if (memo[i] == 0) {
			memo[i] = Fibonacci(i - 1, memo) + Fibonacci(i - 2, memo);
		}
		return memo[i];
	}

public static void Main(String[] args)
{
    for (int i = 1; i <= 35; i++)
    {
        Console.WriteLine(Fibonacci(i));
    }
}
