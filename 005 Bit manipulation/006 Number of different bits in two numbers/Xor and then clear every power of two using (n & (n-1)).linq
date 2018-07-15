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

    public static int BitSwapRequired2(int number1, int number2)
    {
        var count = 0;

        for (var c = number1 ^ number2; c != 0; c = c & (c - 1))
        {
            count++;
        }

        return count;
    }

    public static void Main(String[] args)
    {
        var a = 123432;
        var b = 512132;
        Console.WriteLine(a + ": " + ToFullBinarystring(a));
        Console.WriteLine(b + ": " + ToFullBinarystring(b));

        var nbits2 = BitSwapRequired2(a, b);

        Console.WriteLine("Required number of bits: " + nbits2);
    }
}
