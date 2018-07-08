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

        public static int[] BuildCharFrequencyTable(String phrase)
        {
            int[] table = new int['z' - 'a' + 1];
            foreach (char c in phrase.ToCharArray())
            {
                int x = GetCharNumber(c);
                if (x != -1)
                {
                    table[x]++;
                }
            }
            return table;
        }

        public static bool CheckMaxOneOdd(int[] table)
        {
            bool foundOdd = false;
            foreach (int count in table)
            {
                if (count % 2 == 1)
                {
                    if (foundOdd)
                    {
                        return false;
                    }
                    foundOdd = true;
                }
            }
            return true;
        }

        public static bool IsPermutationOfPalindrome(String phrase)
        {
            int[] table = BuildCharFrequencyTable(phrase);
            return CheckMaxOneOdd(table);
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
                bool a = IsPermutationOfPalindrome(s);
                Console.WriteLine(s);
                Console.WriteLine(a);
				Console.WriteLine();
            }
        }
}
