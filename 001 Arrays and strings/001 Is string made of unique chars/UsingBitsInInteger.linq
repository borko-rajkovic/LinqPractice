<Query Kind="Program" />

/* Assumes only letters a through z. */
        public static Boolean IsUniqueChars(String str)
        {
            if (str.Length > 26)
            { // Only 26 characters
                return false;
            }
            int checker = 0;
            for (int i = 0; i < str.Length; i++)
            {
                int val = str[i] - 'a';
                if ((checker & (1 << val)) > 0) return false;
                checker |= (1 << val);
            }
            return true;
        }

        public static void Main(String[] args)
        {
            String[] words = { "abcde", "hello", "apple", "kite", "padle" };
            foreach (String word in words)
            {
                Console.WriteLine(word + ": " + IsUniqueChars(word));
            }
        }