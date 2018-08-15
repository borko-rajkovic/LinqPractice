<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Boolean DoesMatch(String pattern, String value)
    {
        if (pattern.Length == 0) return value.Length == 0;
        int size = value.Length;

        for (int mainSize = 0; mainSize < size; mainSize++)
        {
            String main = value.Substring(0, mainSize);
            for (int altStart = mainSize; altStart <= size; altStart++)
            {
                for (int altEnd = altStart; altEnd <= size; altEnd++)
                {
                    String alt = value.Substring(altStart, altEnd-altStart);
                    String cand = BuildFromPattern(pattern, main, alt);
                    if (cand.Equals(value))
                    {
                        Console.WriteLine(main + ", " + alt);
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static String BuildFromPattern(String pattern, String main, String alt)
    {
        StringBuilder sb = new StringBuilder();
        char first = pattern[0];
        foreach (char c in pattern.ToCharArray())
        {
            if (c == first)
            {
                sb.Append(main);
            }
            else
            {
                sb.Append(alt);
            }
        }
        return sb.ToString();
    }

    public static void Main(String[] args)
    {
        String[][] tests = new string[][] {
            new string[] { "ababb", "backbatbackbatbat" },
            new string[] { "abab", "backsbatbackbats" },
            new string[] { "aba", "backsbatbacksbat" } };
        foreach (String[] test in tests)
        {
            String pattern = test[0];
            String value = test[1];
            Console.WriteLine(pattern + ", " + value + ": " + DoesMatch(pattern, value));
        }

    }
}
