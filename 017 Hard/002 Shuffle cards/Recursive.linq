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

    public static String ArrayToString(int[] array)
    {
        if (array == null) return "";
        return ArrayToString(array, 0, array.Length - 1);
    }

    public static String ArrayToString(int[] array, int start, int end)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = start; i <= end; i++)
        {
            int v = array[i];
			sb.Append(v);
            if(i!=end) sb.Append(", ");
        }
        return sb.ToString();
    }

    public static int[] ShuffleArrayRecursively(int[] cards, int i)
    {
        if (i == 0)
        {
            return cards;
        }

        /* shuffle elements 0 through index - 1 */
        ShuffleArrayRecursively(cards, i - 1);
        int k = Rand(0, i);

        /* Swap element k and index */
        int temp = cards[k];
        cards[k] = cards[i];
        cards[i] = temp;

        /* Return shuffled array */
        return cards;
    }

    public static void Main(String[] args)
    {
        int[] cards = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        Console.WriteLine(ArrayToString(cards));
        ShuffleArrayRecursively(cards, 9);
        Console.WriteLine(ArrayToString(cards));
    }
}
