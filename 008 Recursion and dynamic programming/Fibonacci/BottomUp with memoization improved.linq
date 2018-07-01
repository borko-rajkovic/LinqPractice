<Query Kind="Program" />

public static int Fibonacci(int n)
{
    if (n == 0) return 0;
    int a = 0;
    int b = 1;
    for (int i = 2; i < n; i++)
    {
        int c = a + b;
        a = b;
        b = c;
    }
    return a + b;
}

public static void Main(String[] args)
{
    for (int i = 1; i <= 35; i++)
    {
        Console.WriteLine(Fibonacci(i));
    }
}
