<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static String InsertInside(String str, int leftIndex)
    {
        String left = str.Substring(0, leftIndex + 1);
        String right = str.Substring(leftIndex + 1, str.Length-(leftIndex + 1));
        return left + "()" + right;
    }

    public static HashSet<String> GenerateParens(int remaining)
    {
        HashSet<String> HashSet = new HashSet<String>();
        if (remaining == 0)
        {
            HashSet.Add("");
        }
        else
        {
            HashSet<String> prev = GenerateParens(remaining - 1);
            foreach (String str in prev)
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (str[i] == '(')
                    {
                        String s = InsertInside(str, i);
                        /* Add s to HashSet if it is not already in there. Note: 	
                         * HashHashSet automatically checks for duplicates before
                         * adding, so an explicit check is not necessary. */
                        HashSet.Add(s);
                    }
                }
                HashSet.Add("()" + str);
            }
        }
        return HashSet;
    }

    public static void Main(String[] args)
    {
        HashSet<String> list = GenerateParens(4);
        foreach (String s in list)
        {
            Console.WriteLine(s);
        }
        Console.WriteLine(list.Count);
    }
}
