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
        Dictionary<int, int> unpairedCount = new Dictionary<int, int>();
        foreach (int x in array)
        {
            int complement = sum - x;

            var keyValue = unpairedCount.ContainsKey(complement) ? unpairedCount[complement] : 0;

            if (keyValue > 0)
            {
                result.Add(new Pair(x, complement));
                AdjustCounterBy(unpairedCount, complement, -1); // decrement complement
            }
            else
            {
                AdjustCounterBy(unpairedCount, x, 1); // increment x
            }
        }
        return result;
    }

    public static void AdjustCounterBy(Dictionary<int, int> counter, int key, int delta)
    {
        var keyValue = counter.ContainsKey(key) ? counter[key] : 0;
        if (counter.ContainsKey(key))
            counter[key] = keyValue + delta;
        else
            counter.Add(key, keyValue + delta);
    }

    public static void Main(String[] args)
    {
        int[] test = { 9, 3, 6, 5, 5, 7, -1, 13, 14, -2, 12, 0 };
        // int[] test = { -1, -1, -1, -1, 0, 0, 0, 0, 1, 1 };
        List<Pair> pairs = PrintPairSums(test, 12);
        foreach (Pair p in pairs)
        {
            Console.WriteLine(p.ToString());
        }
    }
}
