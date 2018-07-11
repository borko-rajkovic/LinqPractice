<Query Kind="Program" />

public class Q1_05_One_Away_A
{
    public static bool OneEditReplace(String s1, String s2)
    {
        bool foundDifference = false;
        for (int i = 0; i < s1.Length; i++)
        {
            if (s1[i] != s2[i])
            {
                if (foundDifference)
                {
                    return false;
                }

                foundDifference = true;
            }
        }
        return true;
    }

    /* Check if you can insert a character into s1 to make s2. */

    public static bool OneEditInsert(String s1, String s2)
    {
        int index1 = 0;
        int index2 = 0;
        while (index2 < s2.Length && index1 < s1.Length)
        {
            if (s1[index1] != s2[index2])
            {
                if (index1 != index2)
                {
                    return false;
                }
                index2++;
            }
            else {
                index1++;
                index2++;
            }
        }
        return true;
    }

    public static bool OneEditAway(String first, String second)
    {
        if (first.Length == second.Length)
        {
            return OneEditReplace(first, second);
        }
        else if (first.Length + 1 == second.Length)
        {
            return OneEditInsert(first, second);
        }
        else if (first.Length - 1 == second.Length)
        {
            return OneEditInsert(second, first);
        }
        return false;
    }

    public static void Main()
    {
        String a = "pse";
        String b = "pale";
        bool isOneEdit = OneEditAway(a, b);
        Console.WriteLine("{0}, {1}: {2}", a, b, isOneEdit);
    }
}
