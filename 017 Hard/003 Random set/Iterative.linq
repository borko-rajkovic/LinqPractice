<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Random random = new Random();

    /* Random number between lower and higher, inclusive */
    public static int Rand(int lower, int higher)
    {
        return lower + random.Next(higher - lower + 1);
    }

    /* pick M elements from original array.*/
    public static int[] PickMIteratively(int[] original, int m)
    {
        int[] subset = new int[m];

        /* Fill in subset array with first part of original array */
        for (int i = 0; i < m; i++)
        {
            subset[i] = original[i];
        }

        /* Go through rest of original array. */
        for (int i = m; i < original.Length; i++)
        {
            int k = Rand(0, i);
            if (k < m)
            {
                subset[k] = original[i];
            }
        }

        return subset;
    }

    public static void Main(String[] args)
    {
        int[] cards = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.WriteLine(string.Join(",", cards));
        int[] set = PickMIteratively(cards, 4);
        Console.WriteLine(string.Join(",", set));
    }
}
