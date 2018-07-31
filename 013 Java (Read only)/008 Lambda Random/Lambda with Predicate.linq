<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Random random = new Random();

    public static bool FlipCoin(int i)
    {
        int r = random.Next(2);

        return r % 2 == 0;
    }

    public static List<int> GetRandomSubset(List<int> list)
    {
        List<int> subset = list.Where(FlipCoin).ToList();
        return subset;
    }

    public static void Main(String[] args)
    {
        List<int> list = new List<int>();
        list.Add(1);
        list.Add(2);
        list.Add(3);
        list.Add(4);
        List<int> subset = GetRandomSubset(list);
        Console.WriteLine(string.Join(",", subset));
    }
}
