<Query Kind="Program" />

class Program
{
    public static List<long> AllPossibleKFactors(int k)
    {
        List<long> values = new List<long>();
        for (int a = 0; a <= k; a++)
        { // 3
            long powA = (int)Math.Pow(3, a);
            for (int b = 0; b <= k; b++)
            { // 5
                long powB = (int)Math.Pow(5, b);
                for (int c = 0; c <= k; c++)
                { // 7
                    long powC = (int)Math.Pow(7, c);
                    long value = powA * powB * powC;
                    if (value < 0 || powA == long.MaxValue || powB == long.MaxValue || powC == long.MaxValue)
                    {
                        value = long.MaxValue;
                    }
                    values.Add(value);
                }
            }
        }
        return values;
    }

    public static long GetKthMagicNumber(int k)
    {
        List<long> possibilities = AllPossibleKFactors(k);
        possibilities.Sort();
        return possibilities[k];
    }

    public static void Main(String[] args)
    {
        for (int i = 0; i < 50; i++)
        {
            Console.WriteLine(i + " : " + GetKthMagicNumber(i));
        }
    }
}
