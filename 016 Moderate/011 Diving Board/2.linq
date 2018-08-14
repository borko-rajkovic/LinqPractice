<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int counter = 0;

    public static HashSet<int> AllLengths(int k, int shorter, int longer)
    {
        HashSet<int> lengths = new HashSet<int>();
        HashSet<String> visited = new HashSet<String>();
        GetAllLengths(k, 0, shorter, longer, lengths, visited);
        return lengths;
    }

    public static void GetAllLengths(int k, int total, int shorter, int longer, HashSet<int> lengths, HashSet<String> visited)
    {
        counter++;
        if (k == 0)
        {
            lengths.Add(total);
            return;
        }
        String key = k + " " + total;
        if (visited.Contains(key))
        {
            return;
        }
        GetAllLengths(k - 1, total + shorter, shorter, longer, lengths, visited);
        GetAllLengths(k - 1, total + longer, shorter, longer, lengths, visited);
        visited.Add(key);
    }

    public static void Main(String[] args)
    {
        HashSet<int> lengths = AllLengths(12, 1, 3);
        Console.WriteLine(string.Join(", ", lengths));
        Console.WriteLine(counter);
    }
}
