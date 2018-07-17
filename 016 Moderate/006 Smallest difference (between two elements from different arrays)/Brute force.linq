<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int FindSmallestDifference(int[] array1, int[] array2)
    {
        if (array1.Length == 0 || array2.Length == 0)
        {
            return -1;
        }
        int min = int.MaxValue;
        for (int i = 0; i < array1.Length; i++)
        {
            for (int j = 0; j < array2.Length; j++)
            {
                if (Math.Abs(array1[i] - array2[j]) < min)
                {
                    min = Math.Abs(array1[i] - array2[j]);
                }
            }
        }
        return min;
    }

    public static void Main(String[] args)
    {
        int[] array1 = { 1, 3, 15, 11, 2 };
        int[] array2 = { 23, 127, 234, 19, 8 };
        int difference = FindSmallestDifference(array1, array2);
        Console.WriteLine(difference);
    }
}
