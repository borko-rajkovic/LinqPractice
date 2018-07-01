<Query Kind="Program" />

        public static Boolean IsUniqueChars(String str)
        {
            if (str.Length > 128)
            {
                return false;
            }
            Boolean[] char_set = new Boolean[128];
            for (int i = 0; i < str.Length; i++)
            {
                int val = str[i];
                if (char_set[val]) return false;
                char_set[val] = true;
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

