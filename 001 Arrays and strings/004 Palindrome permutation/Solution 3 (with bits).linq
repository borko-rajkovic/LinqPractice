<Query Kind="Program" />

public class Q1_04_Palindrome_Permutation
{
	public static int GetCharNumber(char c)
        {
            int a = 'a';
            int z = 'z';

            int val = char.ToLower(c);
            if (a <= val && val <= z)
            {
                return val - a;
            }
            return -1;
        }
		
    /* Toggle the ith bit in the integer. */

    public static int Toggle(int bitVector, int index)
    {
        if (index < 0) return bitVector;

        int mask = 1 << index;
        if ((bitVector & mask) == 0)
        {
            bitVector |= mask;
        }
        else {
            bitVector &= ~mask;
        }
        return bitVector;
    }

    /* Create bit vector for string. For each letter with value i,
     * toggle the ith bit. */

    public static int CreateBitVector(String phrase)
    {
        int bitVector = 0;
        foreach (char c in phrase.ToCharArray())
        {
            int x = GetCharNumber(c);
            bitVector = Toggle(bitVector, x);
        }
        return bitVector;
    }

    /* Check that exactly one bit is set by subtracting one from the
     * integer and ANDing it with the original integer. */

    public static bool CheckExactlyOneBitSet(int bitVector)
    {
        return (bitVector & (bitVector - 1)) == 0;
    }

    public static bool IsPermutationOfPalindrome3(String phrase)
    {
        int bitVector = CreateBitVector(phrase);
        return bitVector == 0 || CheckExactlyOneBitSet(bitVector);
    }

    public static void Main()
    {
        String[] strings = {"Rats live on no evil star",
                        "A man, a plan, a canal, panama",
                        "Lleve",
                        "Tacotac",
                        "asda"};

        foreach (String s in strings)
        {
            bool c = IsPermutationOfPalindrome3(s);
            Console.WriteLine(s);
            Console.WriteLine("Agree: " + c);
            Console.WriteLine();
        }
    }
}
