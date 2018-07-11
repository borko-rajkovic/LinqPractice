<Query Kind="Program" />

public class Q1_05_One_Away_A
{
    public static bool OneEditAway2(String first, String second)
    {
        /* Length checks. */
        if (Math.Abs(first.Length - second.Length) > 1)
        {
            return false;
        }

        /* Get shorter and longer string.*/
        String s1 = first.Length < second.Length ? first : second;
        String s2 = first.Length < second.Length ? second : first;

        int index1 = 0;
        int index2 = 0;
        bool foundDifference = false;
        while (index2 < s2.Length && index1 < s1.Length)
        {
            if (s1[index1] != s2[index2])
            {
                /* Ensure that this is the first difference found.*/
                if (foundDifference) return false;
                foundDifference = true;
                if (s1.Length == s2.Length)
                { // On replace, move shorter pointer
                    index1++;
                }
            }
            else {
                index1++; // If matching, move shorter pointer
            }
            index2++; // Always move pointer for longer string
        }
        return true;
    }

    public static void Main()
    {
        String a = "pse";
        String b = "pale";

        bool isOneEdit2 = OneEditAway2(a, b);
        Console.WriteLine("{0}, {1}: {2}", a, b, isOneEdit2);
    }
}
