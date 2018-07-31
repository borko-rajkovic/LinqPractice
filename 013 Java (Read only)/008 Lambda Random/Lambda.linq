<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<int> GetRandomSubset(List<int> list)
    {
        Random random = new Random();
        List<int> subset = list.Where(k => {
            int r = random.Next(2);
            return r%2==0; /* Flip coin. */
        }).ToList();
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
