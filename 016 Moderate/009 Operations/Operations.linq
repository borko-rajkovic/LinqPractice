<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    /* Flip a positive sign to negative, or a negative sign to pos */
    public static int Negate(int a)
    {
        int neg = 0;
        int newSign = a < 0 ? 1 : -1;
        while (a != 0)
        {
            neg += newSign;
            a += newSign;
        }
        return neg;
    }

    /* Flip a positive sign to negative, or a negative sign to pos */
    public static int NegateOptimized(int a)
    {
        int neg = 0;
        int newSign = a < 0 ? 1 : -1;
        int delta = newSign;
        while (a != 0)
        {
            Boolean differentSigns = (a + delta > 0) != (a > 0);
            if (a + delta != 0 && differentSigns)
            { // If delta is too big, reset it.
                delta = newSign;
            }
            neg += delta;
            a += delta;
            delta += delta; // Double the delta
        }
        return neg;
    }

    /* Subtract two numbers by negating b and adding them */
    public static int Minus(int a, int b)
    {
        return a + Negate(b);
    }

    /* Return absolute value */
    public static int Abs(int a)
    {
        if (a < 0)
        {
            return NegateOptimized(a);
        }
        else return a;
    }

    /* Multiply a by b by adding a to itself b times */
    public static int Multiply(int a, int b)
    {
        if (a < b)
        {
            return Multiply(b, a); // algo is faster if b < a
        }
        int sum = 0;
        for (int i = Abs(b); i > 0; i = Minus(i, 1))
        {
            sum += a;
        }
        if (b < 0)
        {
            sum = NegateOptimized(sum);
        }
        return sum;
    }

    /* Divide a by b by literally counting how many times b can go into
     * a. That is, count how many times you can add b to itself until you reach a. */
    public static int Divide(int a, int b)
    {
        if (b == 0)
        {
            throw new Exception("ERROR: Divide by zero.");
        }
        int absa = Abs(a);
        int absb = Abs(b);

        int product = 0;
        int x = 0;
        while (product + absb <= absa)
        { /* don't go past a */
            product += absb;
            x++;
        }

        if ((a < 0 && b < 0) || (a > 0 && b > 0))
        {
            return x;
        }
        else
        {
            return NegateOptimized(x);
        }
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static void Main(String[] args)
    {
        int minRange = -100;
        int maxRange = 100;
        int cycles = 100;

        for (int i = 0; i < cycles; i++)
        {
            int a = RandomIntInRange(minRange, maxRange);
            int b = RandomIntInRange(minRange, maxRange);
            int ans = Minus(a, b);
            if (ans != a - b)
            {
                Console.WriteLine("ERROR");
            }
            Console.WriteLine(a + " - " + b + " = " + ans);
        }
        for (int i = 0; i < cycles; i++)
        {
            int a = RandomIntInRange(minRange, maxRange);
            int b = RandomIntInRange(minRange, maxRange);
            int ans = Multiply(a, b);
            if (ans != a * b)
            {
                Console.WriteLine("ERROR");
            }
            Console.WriteLine(a + " * " + b + " = " + ans);
        }
        for (int i = 0; i < cycles; i++)
        {
            int a = RandomIntInRange(minRange, maxRange);
            int b = RandomIntInRange(minRange, maxRange);
            Console.Write(a + " / " + b + " = ");
            int ans = Divide(a, b);
            if (ans != a / b)
            {
                Console.WriteLine("ERROR");
            }
            Console.WriteLine(ans);
        }
    }
}
