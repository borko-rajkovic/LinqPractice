<Query Kind="Program" />

public static int AddRecursive(int a, int b)
{
    if (b == 0) return a;
    int sum = a ^ b; // add without carrying
    int carry = (a & b) << 1; // carry, but don�t add
    return AddRecursive(sum, carry); // recurse
}

public static int AddIterative(int a, int b)
{
    while (b != 0)
    {
        int sum = a ^ b; // add without carrying
        int carry = (a & b) << 1; // carry, but don't add			
        a = sum;
        b = carry;
    }
    return a;
}

public static void Main(String[] args)
{
    int a = 50;
    int b = 92;
    int sum;
    sum = AddIterative(a, b);
    Console.WriteLine(a + " + " + b + " = " + sum);
    Console.WriteLine();
    sum = AddRecursive(a, b);
    Console.WriteLine(a + " + " + b + " = " + sum);
}
