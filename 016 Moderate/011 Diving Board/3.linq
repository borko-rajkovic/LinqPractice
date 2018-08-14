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
        for (int nShorter = 0; nShorter <= k; nShorter++)
        {
            counter++;
            int nLonger = k - nShorter;
            int length = nShorter * shorter + nLonger * longer;
            lengths.Add(length);
        }
        return lengths;
    }

    public static void Main(String[] args)
    {
        HashSet<int> lengths = AllLengths(12, 1, 3);
        Console.WriteLine(string.Join(", ", lengths));
        Console.WriteLine(counter);
    }
}
