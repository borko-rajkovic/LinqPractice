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

    public static int? GetTarget(int[] array1, int[] array2)
    {
        int sum1 = Sum(array1);
        int sum2 = Sum(array2);

        if ((sum1 - sum2) % 2 != 0) return null;
        return (sum1 - sum2) / 2;
    }

    public static int[] FindSwapValues(int[] array1, int[] array2)
    {
        int? target = GetTarget(array1, array2);
        if (target == null) return null;
        return FindDifference(array1, array2, target);
    }

    public static int[] FindDifference(int[] array1, int[] array2, int? target)
    {
        Array.Sort(array1);
        Array.Sort(array2);

        int a = 0;
        int b = 0;

        while (a < array1.Length && b < array2.Length)
        {
            int difference = array1[a] - array2[b];
            /* Compare difference to target. If difference is too small, then
             * make it bigger by moving a to a bigger value. If it is too big,
             * then make it smaller by moving b to a bigger value. If it's
             * just right, return this pair. */
            if (difference == target)
            {
                int[] values = { array1[a], array2[b] };
                return values;
            }
            else if (difference < target)
            {
                a++;
            }
            else
            {
                b++;
            }
        }

        return null;
    }

    public static void Main(String[] args)
    {
        //int[] array1 = { -9, -1, -4, 8, 9, 6, -5, -7, 3, 9 };
        //int[] array2 = { 6, 6, 4, -1, 7, -6, -9, 4, -8, 8 };

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
