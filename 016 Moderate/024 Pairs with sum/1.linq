<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Pair
    {
        public int first;
        public int second;

        public Pair(int first, int second)
        {
            this.first = first;
            this.second = second;
        }

        public override String ToString()
        {
            return "(" + first + ", " + second + ")";
        }
    }

    public static List<Pair> PrintPairSums(int[] array, int sum)
    {
        List<Pair> result = new List<Pair>();
        for (int i = 0; i < array.Length; i++)
        {
            for (int j = i + 1; j < array.Length; j++)
            {
                if (array[i] + array[j] == sum)
                {
                    result.Add(new Pair(array[i], array[j]));
                }
            }
        }
        return result;
    }

    public static void Main(String[] args)
    {
        int[] test = { 9, 3, 6, 5, 5, 7, -1, 13, 14, -2, 12, 0 };
        List<Pair> pairs = PrintPairSums(test, 12);
        foreach (Pair p in pairs)
        {
            Console.WriteLine(p.ToString());
        }
    }
}
