<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Boolean GetBit(int num, int i)
    {
        return ((num & (1 << i)) != 0);
    }

    public static int LongestSequence(int n)
    {
        int maxSeq = 0;

        for (int i = 0; i < sizeof(int) * 8; i++)
        {
            maxSeq = Math.Max(maxSeq, LongestSequenceOf1s(n, i));
        }

        return maxSeq;
    }

    public static int LongestSequenceOf1s(int n, int indexToIgnore)
    {
        int max = 0;
        int counter = 0;
        for (int i = 0; i < sizeof(int) * 8; i++)
        {
            if (i == indexToIgnore || GetBit(n, i))
            {
                counter++;
                max = Math.Max(counter, max);
            }
            else
            {
                counter = 0;
            }
        }
        return max;
    }

    public static void Main(String[] args)
    {
        int original_number = 1775;
        int new_number = LongestSequence(original_number);

        Console.WriteLine(Convert.ToString(original_number, 2));
        Console.WriteLine(new_number);
    }
}
