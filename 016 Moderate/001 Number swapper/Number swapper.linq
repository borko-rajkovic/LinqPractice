<Query Kind="Program" />

public static void Swap(int a, int b)
{
    // Example for a = 9, b = 4
    a = a - b; // a = 9 - 4 = 5
    b = a + b; // b = 5 + 4 = 9
    a = b - a; // a = 9 - 5

    Console.WriteLine("a: " + a);
    Console.WriteLine("b: " + b);
}

public static void SwapOpt(int a, int b)
{
    a = a ^ b;
    b = a ^ b;
    a = a ^ b;

    Console.WriteLine("a: " + a);
    Console.WriteLine("b: " + b);
}


public static void Main(String[] args)
{
    int a = 30;
    int b = 40;

    Console.WriteLine("a: " + a);
    Console.WriteLine("b: " + b);

    Swap(a, b);
    SwapOpt(a, b);
}
