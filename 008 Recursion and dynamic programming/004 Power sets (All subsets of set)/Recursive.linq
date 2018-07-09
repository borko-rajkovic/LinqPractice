<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<List<int>> GetSubsets(List<int> set, int index)
    {
        List<List<int>> allsubsets;
        if (set.Count == index)
        { // Base case - add empty set
            allsubsets = new List<List<int>>();
            allsubsets.Add(new List<int>());
        }
        else
        {
            allsubsets = GetSubsets(set, index + 1);
            int item = set[index];
            List<List<int>> moresubsets = new List<List<int>>();
            foreach (List<int> subset in allsubsets)
            {
                List<int> newsubset = new List<int>();
                newsubset.AddRange(subset);
                newsubset.Add(item);
                moresubsets.Add(newsubset);
            }
            allsubsets.AddRange(moresubsets);
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
        List<List<int>> subsets = GetSubsets(list, 0);
        PrintSubsets(subsets);
    }
}
