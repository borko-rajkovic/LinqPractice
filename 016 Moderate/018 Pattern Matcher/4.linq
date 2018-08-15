<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
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
            if (countOfAlt == 0 || remainingLength % countOfAlt == 0)
            {
                int altIndex = firstAlt * mainSize;
                int altSize = countOfAlt == 0 ? 0 : remainingLength / countOfAlt;
                if (Matches(pattern, value, mainSize, altSize, altIndex))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static Boolean Matches(String pattern, String value, int mainSize, int altSize, int firstAlt)
    {
        int stringIndex = mainSize;
        for (int i = 1; i < pattern.Length; i++)
        {
            int size = pattern[i] == pattern[0] ? mainSize : altSize;
            int offset = pattern[i] == pattern[0] ? 0 : firstAlt;
            if (!IsEqual(value, offset, stringIndex, size))
            {
                return false;
            }
            stringIndex += size;
        }
        return true;
    }

    public static Boolean IsEqual(String s1, int offset1, int offset2, int size)
    {
        for (int i = 0; i < size; i++)
        {
            if (s1[offset1 + i] != s1[offset2 + i])
            {
                return false;
            }
        }
        return true;
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
