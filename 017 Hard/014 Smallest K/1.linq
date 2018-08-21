<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int[] SmallestK(int[] array, int k)
    {
        if (k <= 0 || k > array.Length)
        {
            throw new Exception();
        }

        /* Sort array. */
        Array.Sort(array);

        /* Copy first k elements. */
        int[] smallest = new int[k];
        for (int i = 0; i < k; i++)
        {
            smallest[i] = array[i];
        }
        return smallest;
    }

    public static void Main(String[] args)
    {
        int[] array = { 1, 5, 2, 9, 1, 11, 6, 13, 15 };
        int[] smallest = SmallestK(array, 3);
        Console.WriteLine(string.Join(", ", smallest));
    }
}
