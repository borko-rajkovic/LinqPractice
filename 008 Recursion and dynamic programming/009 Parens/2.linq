<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static void AddParen(List<String> list, int leftRem, int rightRem, char[] str, int index)
    {
        if (leftRem < 0 || rightRem < leftRem) return; // invalid state

        if (leftRem == 0 && rightRem == 0)
        { /* all out of left and right parentheses */
            list.Add(new string(str));
        }
        else
        {
            str[index] = '('; // Add left and recurse
            AddParen(list, leftRem - 1, rightRem, str, index + 1);

            str[index] = ')'; // Add right and recurse
            AddParen(list, leftRem, rightRem - 1, str, index + 1);
        }
    }

    public static List<String> GenerateParens(int count)
    {
        char[] str = new char[count * 2];
        List<String> list = new List<String>();
        AddParen(list, count, count, str, 0);
        return list;
    }

    public static void Main(String[] args)
    {
        List<String> list = GenerateParens(4);
        foreach (String s in list)
        {
            Console.WriteLine(s);
        }
        Console.WriteLine(list.Count);
    }
}
