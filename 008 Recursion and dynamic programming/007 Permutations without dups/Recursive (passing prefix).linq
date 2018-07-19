<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static void GetPerms(String prefix, String remainder, List<String> result)
    {
        if (remainder.Length == 0)
        {
            result.Add(prefix);
        }
        int len = remainder.Length;
        for (int i = 0; i < len; i++)
        {
            String before = remainder.Substring(0, i);
            String after = remainder.Substring(i + 1, len-(i+1));
            char c = remainder[i];
            GetPerms(prefix + c, before + after, result);
        }
    }

    public static List<String> GetPerms(String str)
    {
        List<String> result = new List<String>();
        GetPerms("", str, result);
        return result;
    }

    public static void Main(String[] args)
    {
        List<String> list = GetPerms("abcde");
        Console.WriteLine("There are " + list.Count + " permutations.");
        foreach (String s in list)
        {
            Console.WriteLine(s);
        }
    }
}
