<Query Kind="Program" />

class Program
{
    public static int counter = 0;

    public static int Sum(int x, int y)
    {
        counter++;
        return x + y;
    }

    public static int MinProductHelper(int smaller, int bigger)
    {
        if (smaller == 0)
        { // 0 x bigger = 0
            return 0;
        }
        else if (smaller == 1)
        { // 1 x bigger = bigger
            return bigger;
        }

        /* Compute half. If uneven, compute other half. If even,
         * double it. */
        int s = smaller >> 1; // Divide by 2
        int side1 = MinProductHelper(s, bigger);
        int side2 = side1;
        if (smaller % 2 == 1)
        {
            counter++;
            side2 = MinProductHelper(smaller - s, bigger);
        }
        counter++;
        return side1 + side2;
    }

    public static int MinProduct(int a, int b)
    {
        int bigger = a < b ? b : a;
        int smaller = a < b ? a : b;
        return MinProductHelper(smaller, bigger);
    }

    public static void Main(String[] args)
    {
		counter=0;
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
