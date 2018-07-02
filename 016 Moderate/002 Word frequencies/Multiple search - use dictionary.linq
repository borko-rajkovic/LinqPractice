<Query Kind="Program" />

public class Program
{
    public static String GetLongTextBlob()
    {
        String book = "As they rounded a bend in the path that ran beside the river, Lara recognized the silhouette of a fig tree atop a nearby hill. The weather was hot and the days were long. The fig tree was in full leaf, but not yet bearing fruit. "
                + "Soon Lara spotted other landmarks�an outcropping of limestone beside the path that had a silhouette like a man�s face, a marshy spot beside the river where the waterfowl were easily startled, a tall tree that looked like a man with his arms upraised. They were drawing near to the place where there was an island in the river. The island was a good spot to make camp. They would sleep on the island tonight."
                + "Lara had been back and forth along the river path many times in her short life. Her people had not created the path�it had always been there, like the river�but their deerskin-shod feet and the wooden wheels of their handcarts kept the path well worn. Lara�s people were salt traders, and their livelihood took them on a continual journey. ";
        String book_mod = book.Replace('.', ' ').Replace(',', ' ')
                .Replace('-', ' ');
        return book_mod;
    }

    public static String[] GetLongTextBlobAsStringList()
    {
        return GetLongTextBlob().Split(' ');
    }

    public static Dictionary<String, int> SetupDictionary(String[] book)
    {
        Dictionary<String, int> table = new Dictionary<string, int>();
        foreach (String w in book)
        {
            string word = w.ToLower();
            if (word.Trim() != "")
            {
                if (!table.ContainsKey(word))
                {
                    table.Add(word, 0);
                }
                table[word] = table[word] + 1;
            }
        }
        return table;
    }

    public static int GetFrequency(Dictionary<String, int> table, String word)
    {
        if (table == null || word == null)
        {
            return -1;
        }
        word = word.ToLower();
        if (table.ContainsKey(word))
        {
            return table[word];
        }
        return 0;
    }

    public static void Main(String[] args)
    {
        String[] wordlist = GetLongTextBlobAsStringList();
        Dictionary<String, int> dictionary = SetupDictionary(wordlist);

        String[] words = { "the", "Lara", "and", "outcropping", "career", "it" };
        foreach (String word in words)
        {
            Console.WriteLine(word + ": " + GetFrequency(dictionary, word));
        }
    }
}
