<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
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

    public static int CountOf(String pattern, char c)
    {
        int count = 0;
        for (int i = 0; i < pattern.Length; i++)
        {
            if (pattern[i] == c)
            {
                count++;
            }
        }
        return count;
    }

    public static Boolean DoesMatch(String pattern, String value)
    {
        if (pattern.Length == 0) return value.Length == 0;

        char mainChar = pattern[0];
        char altChar = mainChar == 'a' ? 'b' : 'a';
        int size = value.Length;

        int countOfMain = CountOf(pattern, mainChar);
        int countOfAlt = pattern.Length - countOfMain;
        int firstAlt = pattern.IndexOf(altChar);
        int maxMainSize = size / countOfMain;

        for (int mainSize = 0; mainSize <= maxMainSize; mainSize++)
        {
            int remainingLength = size - mainSize * countOfMain;
            String first = value.Substring(0, mainSize);
            if (countOfAlt == 0 || remainingLength % countOfAlt == 0)
            {
                int altIndex = firstAlt * mainSize;
                int altSize = countOfAlt == 0 ? 0 : remainingLength / countOfAlt;
                String second = countOfAlt == 0 ? "" : value.Substring(altIndex, altSize);

                String candidate = BuildFromPattern(pattern, first, second);

                if (candidate.Equals(value))
                {
                    Console.WriteLine(first + ", " + second);
                    return true;
                }
            }
        }
        return false;
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
