<Query Kind="Program" />

class Program
{
    public static int counter = 0;

    public static int MinProduct(int a, int b)
    {
        if (a < b) return MinProduct(b, a);
        int value = 0;
        while (a > 0)
        {
            counter++;
            if ((a % 10) % 2 == 1)
            {
                value += b;
            }
            a >>= 1;
            b <<= 1;
        }
        return value;
    }

    public static void Main(String[] args)
    {
		counter = 0;
        int a = 13494;
        int b = 22323;
        int product = a * b;
        int minProduct = MinProduct(a, b);
        if (product == minProduct)
        {
            Console.WriteLine("Success: " + a + " * " + b + " = " + product);
        }
        else
        {
            Console.WriteLine("Failure: " + a + " * " + b + " = " + product + " instead of " + minProduct);
        }
        Console.WriteLine("Adds: " + counter);

    }
}