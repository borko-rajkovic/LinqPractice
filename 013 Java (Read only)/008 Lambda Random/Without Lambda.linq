<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<int> GetRandomSubset(List<int> list)
    {
        List<int> subset = new List<int>();
        Random random = new Random();
        foreach (int item in list)
        {
            int r = random.Next(2);
            if (r%2==0)
            {
                subset.Add(item);
            }
        }
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
