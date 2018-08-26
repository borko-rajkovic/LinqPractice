<Query Kind="Program" />

class Program
{
    /* find path to transform startWord into endWord. */
    public static LinkedList<String> Transform(String start, String stop, String[] words)
    {
        Dictionary<String, List<String>> wildcardToWordList = CreateWildcardToWordMap(words);
        HashSet<String> visited = new HashSet<String>();
        return Transform(visited, start, stop, wildcardToWordList);
    }

    /* Do a depth-first search from start to stop, traveling through each word that is one edit away. */
    public static LinkedList<String> Transform(HashSet<String> visited, String start, String stop, Dictionary<String, List<String>> wildcardToWordList)
    {
        if (start.Equals(stop))
        {
            LinkedList<String> path = new LinkedList<String>();
            path.AddFirst(start);
            return path;
        }
        else if (visited.Contains(start))
        {
            return null;
        }

        visited.Add(start);
        List<String> words = GetValidLinkedWords(start, wildcardToWordList);

        foreach (String word in words)
        {
            LinkedList<String> path = Transform(visited, word, stop, wildcardToWordList);
            if (path != null)
            {
                path.AddFirst(start);
                return path;
            }
        }

        return null;
    }

    /* Insert words in dictionary into mapping from wildcard form -> word. */
    public static Dictionary<String, List<String>> CreateWildcardToWordMap(String[] words)
    {
        Dictionary<String, List<String>> wildcardToWords = new Dictionary<String, List<String>>();
        foreach (String word in words)
        {
            List<String> linked = GetWildcardRoots(word);
            foreach (String linkedWord in linked)
            {
                if (!wildcardToWords.ContainsKey(linkedWord))
                {
                    wildcardToWords.Add(linkedWord, new List<string>());
                }
                wildcardToWords[linkedWord].Add(word);
            }
        }
        return wildcardToWords;
    }

    /* Get list of wildcards associated with word. */
    public static List<String> GetWildcardRoots(String w)
    {
        List<String> words = new List<String>();
        for (int i = 0; i < w.Length; i++)
        {
            String word = w.Substring(0, i) + "_" + w.Substring(i + 1);
            words.Add(word);
        }
        return words;
    }

    /* Return words that are one edit away. */
    public static List<String> GetValidLinkedWords(String word, Dictionary<String, List<String>> wildcardToWords)
    {
        List<String> wildcards = GetWildcardRoots(word);
        List<String> linkedWords = new List<String>();
        foreach (String wildcard in wildcards)
        {
            List<String> words = wildcardToWords[wildcard];
            foreach (String linkedWord in words)
            {
                if (!linkedWord.Equals(word))
                {
                    linkedWords.Add(linkedWord);
                }
            }
        }
        return linkedWords;
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
            Console.WriteLine(string.Join(", ", list));
        }

    }
}
