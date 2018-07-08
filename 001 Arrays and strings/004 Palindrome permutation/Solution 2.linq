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

        public static bool IsPermutationOfPalindrome2(String phrase)
        {
            int countOdd = 0;
            int[] table = new int['z' - 'a' + 1];
            foreach (char c in phrase.ToCharArray())
            {
                int x = GetCharNumber(c);
                if (x != -1)
                {
                    table[x]++;

                    if (table[x] % 2 == 1)
                    {
                        countOdd++;
                    }
                    else
                    {
                        countOdd--;
                    }
                }
            }
            return countOdd <= 1;
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
                bool b = IsPermutationOfPalindrome2(s);
                Console.WriteLine(s);
                Console.WriteLine("Agree: " + b);
                Console.WriteLine();
            }
        }
}
