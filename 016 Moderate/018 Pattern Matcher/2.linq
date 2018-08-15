<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static String FormStringFromPattern(String pattern, String first, String second)
    {
        if (pattern.Length == 0) return null;

        StringBuilder sb = new StringBuilder();
        char firstChar = pattern[0];
        foreach (char c in pattern.ToCharArray())
        {
            if (c == firstChar)
            {
                sb.Append(first);
            }
            else if (c != firstChar)
            {
                sb.Append(second);
            }
            else
            {
                return null;
            }
        }
        return sb.ToString();
    }

    public static int CountOf(String pattern, char ch)
    {
        int count = 0;
        foreach (char c in pattern.ToCharArray())
        {
            if (c == ch)
            {
                count++;
            }
        }
        return count;
    }

    public static String Canonical(String pattern)
    {
        if (pattern[0] == 'a') return pattern;
        StringBuilder sb = new StringBuilder();
        foreach (char c in pattern.ToCharArray())
        {
            if (c == 'a')
            {
                sb.Append('b');
            }
            else
            {
                sb.Append('a');
            }
        }
        return sb.ToString();
    }

    public static Boolean DoesMatch(String pattern, String value)
    {
        if (pattern.Length == 0) return value.Length == 0;

        pattern = Canonical(pattern);

        int countOfAs = CountOf(pattern, 'a');
        int countOfBs = pattern.Length - countOfAs;
        int firstB = pattern.IndexOf('b');

        for (int aSize = 0; aSize <= value.Length / countOfAs; aSize++)
        {
            int remainingLength = value.Length - aSize * countOfAs;
            String first = value.Substring(0, aSize);
            if (countOfBs == 0 || remainingLength % countOfBs == 0)
            {
                int bIndex = firstB * aSize;
                int bSize = countOfBs == 0 ? 0 : remainingLength / countOfBs;
                String second = countOfBs == 0 ? "" : value.Substring(bIndex, bSize);

                String candidate = FormStringFromPattern(pattern, first, second);

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
