<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<String> GetPerms(String remainder)
    {
        int len = remainder.Length;
        List<String> result = new List<String>();

        /* Base case. */
        if (len == 0)
        {
            result.Add(""); // Be sure to return empty string!
            return result;
        }


        for (int i = 0; i < len; i++)
        {
            /* Remove char i and find permutations of remaining characters.*/
            String before = remainder.Substring(0, i);
            String after = remainder.Substring(i + 1, len - (i+1));
            List<String> partials = GetPerms(before + after);

            /* Prepend char i to each permutation.*/
            foreach (String s in partials)
            {
                result.Add(remainder[i] + s);
            }
        }

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
