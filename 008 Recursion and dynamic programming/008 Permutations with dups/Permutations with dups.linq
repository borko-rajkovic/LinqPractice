<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Dictionary<Char, int> BuildFreqTable(String s)
    {
        Dictionary<Char, int> map = new Dictionary<Char, int>();
        foreach (char c in s.ToCharArray())
        {
            if (!map.ContainsKey(c))
            {
                map.Add(c, 0);
            }
            map[c]++;            
        }
        return map;
    }

    public static void PrintPerms(Dictionary<Char, int> map, String prefix, int remaining, List<String> result)
    {
        if (remaining == 0)
        {
            result.Add(prefix);
            return;
        }

        List<Char> keys = new List<Char> (map.Keys);

        for (int i = 0; i < keys.Count; i++)
        {
            Char c = keys[i];
            int count = map[c];
            if (count > 0)
            {
                map[c] = count - 1;
                PrintPerms(map, prefix + c, remaining - 1, result);
                map[c] = count;
            }
        }
    }

    public static List<String> PrintPerms(String s)
    {
        List<String> result = new List<String>();
        Dictionary<Char, int> map = BuildFreqTable(s);
        PrintPerms(map, "", s.Length, result);
        return result;
    }

    public static void Main(String[] args)
    {
        String s = "aabbccc";
        List<String> result = PrintPerms(s);
        Console.WriteLine("Count: " + result.Count);
        foreach (String r in result)
        {
            Console.WriteLine(r);
        }
    }
}
