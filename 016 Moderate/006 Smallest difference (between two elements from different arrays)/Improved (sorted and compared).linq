<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int FindSmallestDifference(int[] array1, int[] array2)
    {
        Array.Sort(array1);
        Array.Sort(array2);
        int a = 0;
        int b = 0;
        int difference = int.MaxValue;
        while (a < array1.Length && b < array2.Length)
        {
            if (Math.Abs(array1[a] - array2[b]) < difference)
            {
                difference = Math.Abs(array1[a] - array2[b]);
                if (difference == 0) return difference;
            }
            if (array1[a] < array2[b])
            {
                a++;
            }
            else
            {
                b++;
            }
        }
        return difference;
    }

    public static void Main(String[] args)
    {
        int[] array1 = { 1, 3, 15, 11, 2 };
        int[] array2 = { 23, 127, 234, 19, 8 };
        int difference = FindSmallestDifference(array1, array2);
        Console.WriteLine(difference);
    }
}
