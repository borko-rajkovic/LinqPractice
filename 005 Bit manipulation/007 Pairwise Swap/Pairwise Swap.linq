<Query Kind="Program" />

public class Program
{
    public static string ToFullBinarystring(int number)
    {
        var binaryString = "";

        for (var i = 0; i < 32; i++)
        {
            var lsb = number & 1;
            binaryString = string.Format("{0}{1}", lsb, binaryString);
            number = number >> 1;
        }

        return binaryString;
    }

    public static int SwapOddEvenBits(uint x)
    {
        return (int)(((x & 0xaaaaaaaa) >> 1) | ((x & 0x55555555) << 1));
    }

    public static void Main(String[] args)
    {
        int a, b;

        a = 234321 + 0x40000000;
        Console.WriteLine(a + ": \t" + ToFullBinarystring(a));
        b = SwapOddEvenBits((uint)a);
        Console.WriteLine(b + ": \t" + ToFullBinarystring(b));

        a = 234321 + (1 << 31);
        Console.WriteLine(a + ": \t" + ToFullBinarystring(a));
        b = SwapOddEvenBits((uint)a);
        Console.WriteLine(b + ": \t" + ToFullBinarystring(b));

    }
}
