<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int Sum(int[] array)
    {
        int s = 0;
        foreach (int a in array)
        {
            s += a;
        }
        return s;
    }

    public static int[] FindSwapValues(int[] array1, int[] array2)
    {
        int sum1 = Sum(array1);
        int sum2 = Sum(array2);

        foreach (int one in array1)
        {
            foreach (int two in array2)
            {
                int newSum1 = sum1 - one + two;
                int newSum2 = sum2 - two + one;
                if (newSum1 == newSum2)
                {
                    int[] values = { one, two };
                    return values;
                }
            }
        }

        return null;
    }

    public static void Main(String[] args)
    {
        int[] array1 = { 1, 1, 1, 2, 2, 4 };
        int[] array2 = { 3, 3, 3, 6 };
        int[] swaps = FindSwapValues(array1, array2);
        if (swaps == null)
        {
            Console.WriteLine("null");
        }
        else
        {
            Console.WriteLine(swaps[0] + " " + swaps[1]);
        }

    }
}
