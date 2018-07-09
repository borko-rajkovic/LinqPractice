<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<int> ConvertIntToSet(int x, List<int> set)
    {
        List<int> subset = new List<int>();
        int index = 0;
        for (int k = x; k > 0; k >>= 1)
        {
            if ((k & 1) == 1)
            {
                subset.Add(set[index]);
            }
            index++;
        }
        return subset;
    }

    public static List<List<int>> GetSubsets(List<int> set)
    {
        List<List<int>> allsubsets = new List<List<int>>();
        int max = 1 << set.Count; /* Compute 2^n */
        for (int k = 0; k < max; k++)
        {
            List<int> subset = ConvertIntToSet(k, set);
            allsubsets.Add(subset);
        }
        return allsubsets;
    }

    public static void PrintSubsets(List<List<int>> subsets)
    {
        foreach (var subset in subsets)
        {
            Console.WriteLine("Subset");
            foreach (var item in subset)
            {
                Console.Write(item+" ");
            }
            if(subset.Count == 0)
            {
                Console.Write("[empty]");
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }

    public static void Main(String[] args)
    {
        List<int> list = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            list.Add(i+5);
        }
        List<List<int>> subsets = GetSubsets(list);
        PrintSubsets(subsets);
    }
}
