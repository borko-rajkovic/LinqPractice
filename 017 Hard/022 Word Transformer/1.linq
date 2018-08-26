<Query Kind="Program" />

class Program
{
    public static List<String> WordsOneAway(String word)
    {
        List<String> words = new List<String>();
        for (int i = 0; i < word.Length; i++)
        {
            for (char c = 'a'; c <= 'z'; c++)
            {
                String w = word.Substring(0, i) + c + word.Substring(i + 1);
                words.Add(w);
            }
        }
        return words;
    }

    public static LinkedList<String> Transform(HashSet<String> visited, String startWord, String stopWord, HashSet<String> dictionary)
    {
        if (startWord.Equals(stopWord))
        {
            LinkedList<String> path = new LinkedList<String>();
            path.AddFirst(startWord);
            return path;
        }
        else if (visited.Contains(startWord) || !dictionary.Contains(startWord))
        {
            return null;
        }

        visited.Add(startWord);
        List<String> words = WordsOneAway(startWord);

        foreach (String word in words)
        {
            LinkedList<String> path = Transform(visited, word, stopWord, dictionary);
            if (path != null)
            {
                path.AddFirst(startWord);
                return path;
            }
        }

        return null;
    }

    public static LinkedList<String> Transform(String start, String stop, String[] words)
    {
        HashSet<String> dict = SetupDictionary(words);
        HashSet<String> visited = new HashSet<String>();
        return Transform(visited, start, stop, dict);
    }

    public static HashSet<String> SetupDictionary(String[] words)
    {
        HashSet<String> hash = new HashSet<String>();
        foreach (String word in words)
        {
            hash.Add(word.ToLower());
        }
        return hash;
    }

    public static void Main(String[] args)
    {
        String[] words = { "maps", "tan", "tree", "apple", "cans", "help", "aped", "pree", "pret", "apes", "flat", "trap", "fret", "trip", "trie", "frat", "fril" };
        LinkedList<String> list = Transform("tree", "flat", words);

        if (list == null)
        {
            Console.WriteLine("No path.");
        }
        else
        {
            Console.WriteLine(String.Join(", ", list));
        }
    }
}
