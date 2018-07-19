<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static List<String> GetPerms(String str)
    {
        if (str == null)
        {
            return null;
        }
        List<String> permutations = new List<String>();
        if (str.Length == 0)
        { // base case
            permutations.Add("");
            return permutations;
        }

        char first = str[0]; // get the first character
        String remainder = str.Substring(1); // remove the first character
        List<String> words = GetPerms(remainder);
        foreach (String word in words)
        {
            for (int j = 0; j <= word.Length; j++)
            {
                String s = InsertCharAt(word, first, j);
                permutations.Add(s);
            }
        }
        return permutations;
    }

    public static String InsertCharAt(String word, char c, int i)
    {
        String start = word.Substring(0, i);
        String end = word.Substring(i);
        return start + c + end;
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
