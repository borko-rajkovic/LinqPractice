<Query Kind="Program" />

public static int Fibonacci(int i) {
		if (i == 0) {
			return 0;
		}
		if (i == 1) {
			return 1;
		}
		return Fibonacci(i - 1) + Fibonacci(i - 2);
	}
	
public static void Main(String[] args)
{
    for (int i = 1; i <= 35; i++)
    {
        Console.WriteLine(Fibonacci(i));
    }
}
