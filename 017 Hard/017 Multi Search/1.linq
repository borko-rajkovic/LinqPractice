<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Boolean IsSubstringAtLocation(String big, String small, int offset)
    {
        for (int i = 0; i < small.Length; i++)
        {
            if (big[offset + i] != small[i])
            {
                return false;
            }
        }
        return true;
    }

    public static List<int> Search(String big, String small)
    {
        List<int> locations = new List<int>();
        for (int i = 0; i < big.Length - small.Length + 1; i++)
        {
            if (IsSubstringAtLocation(big, small, i))
            {
                locations.Add(i);
            }
        }
        return locations;
    }

    public static Dictionary<String, List<int>> SearchAll(String big, String[] smalls)
    {
        Dictionary<String, List<int>> lookup = new Dictionary<String, List<int>>();
        foreach (String small in smalls)
        {
            List<int> locations = Search(big, small);
            lookup.Add(small, locations);
        }
        return lookup;
    }

    public static void Main(String[] args)
    {
        String big = "mississippi";
        String[] smalls = { "is", "ppi", "hi", "sis", "i", "mississippi" };
        Dictionary<String, List<int>> locations = SearchAll(big, smalls);
        foreach (var location in locations)
        {
            Console.Write(location.Key+": ");
            Console.Write(string.Join(", ", location.Value));
            Console.WriteLine();
        }
    }
}
