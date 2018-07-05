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

    /* pick M elements from original array, using only elements 0 through i (inclusive).*/
    public static int[] PickMRecursively(int[] original, int m, int i)
    {
        if (i + 1 < m)
        { // Not enough elements
            return null;
        }
        else if (i + 1 == m)
        { // Base case -- copy first m elements into array
            int[] set = new int[m];
            for (int k = 0; k < m; k++)
            {
                set[k] = original[k];
            }
            return set;
        }
        else
        {
            int[] set = PickMRecursively(original, m, i - 1);
            int k = Rand(0, i);
            if (k < m)
            {
                set[k] = original[i];
            }
            return set;
        }
    }

    public static void Main(String[] args)
    {
        int[] cards = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.WriteLine(string.Join(",", cards));
        int[] set = PickMRecursively(cards, 4, 9);
        Console.WriteLine(string.Join(",", set));
    }
}
