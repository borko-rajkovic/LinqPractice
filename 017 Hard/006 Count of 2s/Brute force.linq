<Query Kind="Program" />

public class Program
{
    public static int NumberOf2s(int n)
    {
        int count = 0;
        while (n > 0)
        {
            if (n % 10 == 2)
            {
                count++;
            }
            n = n / 10;
        }
        return count;
    }

    public static int NumberOf2sInRange(int n)
    {
        int count = 0;
        for (int i = 2; i <= n; i++)
        { // Might as well start at 2
            count += NumberOf2s(i);
        }
        return count;
    }

    public static void Main(String[] args)
    {
        for (int i = 10000000-10; i < 10000000; i++)
        {
            int v = NumberOf2sInRange(i);
            Console.WriteLine("Between 0 and " + i + ": " + v);
        }
    }
}
