<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static Boolean IsSubstring(String big, String small)
    {
        if (big.IndexOf(small) >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static Boolean IsRotation(String s1, String s2)
    {
        int len = s1.Length;
        /* check that s1 and s2 are equal length and not empty */
        if (len == s2.Length && len > 0)
        {
            /* concatenate s1 and s1 within new buffer */
            String s1s1 = s1 + s1;
            return IsSubstring(s1s1, s2);
        }
        return false;
    }

    public static void Main(String[] args)
    {
        String[][] pairs = { new string[] { "apple", "pleap" }, new string[] { "waterbottle", "erbottlewat" }, new string[] { "camera", "macera" } };
        foreach (String[] pair in pairs)
        {
            String word1 = pair[0];
            String word2 = pair[1];
            Boolean is_rotation = IsRotation(word1, word2);
            Console.WriteLine(word1 + ", " + word2 + ": " + is_rotation);
        }

    }
}
