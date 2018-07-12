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
        {
            return 0;
        }
        else if (smaller == 1)
        {
            return bigger;
        }

        int s = smaller >> 1;
        int halfProd = MinProductHelper(s, bigger);

        if (smaller % 2 == 0)
        {
            counter++;
            return halfProd + halfProd;
        }
        else
        {
            counter += 2;
            return halfProd + halfProd + bigger;
        }
    }


    public static int MinProduct(int a, int b)
    {
		counter = 0;
        int bigger = a < b ? b : a;
        int smaller = a < b ? a : b;

        return MinProductHelper(smaller, bigger);
    }

    public static void Main(String[] args)
    {
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
