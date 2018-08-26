<Query Kind="Program" />

class Program
{
    public class PathNode
    {
        private String word = null;
        private PathNode previousNode = null;
        public PathNode(String word, PathNode previous)
        {
            this.word = word;
            previousNode = previous;
        }

        public String GetWord()
        {
            return word;
        }

        /* Traverse path and return linked list of nodes. */
        public LinkedList<String> Collapse(Boolean startsWithRoot)
        {
            LinkedList<String> path = new LinkedList<String>();
            PathNode node = this;
            while (node != null)
            {
                if (startsWithRoot)
                {
                    path.AddLast(node.word);
                }
                else
                {
                    path.AddFirst(node.word);
                }
                node = node.previousNode;
            }
            return path;
        }
    }

    public class BFSData
    {
        public Queue<PathNode> toVisit = new Queue<PathNode>();
        public Dictionary<String, PathNode> visited = new Dictionary<String, PathNode>();

        public BFSData(String root)
        {
            PathNode sourcePath = new PathNode(root, null);
            toVisit.Enqueue(sourcePath);
            visited.Add(root, sourcePath);
        }

        public Boolean IsFinished()
        {
            return toVisit.Count == 0;
        }
    }

    public static LinkedList<String> Transform(String startWord, String stopWord, String[] words)
    {
        Dictionary<String, List<String>> wildcardToWordList = GetWildcardToWordList(words);

        BFSData sourceData = new BFSData(startWord);
        BFSData destData = new BFSData(stopWord);

        while (!sourceData.IsFinished() && !destData.IsFinished())
        {
            /* Search out from source. */
            String collision = SearchLevel(wildcardToWordList, sourceData, destData);
            if (collision != null)
            {
                return MergePaths(sourceData, destData, collision);
            }

            /* Search out from destination. */
            collision = SearchLevel(wildcardToWordList, destData, sourceData);
            if (collision != null)
            {
                return MergePaths(sourceData, destData, collision);
            }
        }

        return null;
    }

    /* Search one level and return collision, if any. */
    public static String SearchLevel(Dictionary<String, List<String>> wildcardToWordList, BFSData primary, BFSData secondary)
    {
        /* We only want to search one level at a time. Count how many nodes are currently in the primary's
         * level and only do that many nodes. We'll continue to add nodes to the end. */
        int count = primary.toVisit.Count;
        for (int i = 0; i < count; i++)
        {
            /* Pull out first node. */
            PathNode pathNode = primary.toVisit.Dequeue();
            String word = pathNode.GetWord();

            /* Check if it's already been visited. */
            if (secondary.visited.ContainsKey(word))
            {
                return pathNode.GetWord();
            }

            /* Add friends to queue. */
            List<String> words = GetValidLinkedWords(word, wildcardToWordList);
            foreach (String w in words)
            {
                if (!primary.visited.ContainsKey(w))
                {
                    PathNode next = new PathNode(w, pathNode);
                    primary.visited.Add(w, next);
                    primary.toVisit.Enqueue(next);
                }
            }
        }
        return null;
    }

    public static LinkedList<String> MergePaths(BFSData bfs1, BFSData bfs2, String connection)
    {
        PathNode end1 = bfs1.visited[connection]; // end1 -> source
        PathNode end2 = bfs2.visited[connection]; // end2 -> dest
        LinkedList<String> pathOne = end1.Collapse(false); // forward: source -> connection
        LinkedList<String> pathTwo = end2.Collapse(true); // reverse: connection -> dest
        pathTwo.RemoveFirst(); // remove connection
        // pathOne.addAll(pathTwo); // add second path
        LinkedList<String> mergedPath = new LinkedList<string>();
        string[] pathOneArray = new string[pathOne.Count];
        pathOne.CopyTo(pathOneArray, 0);
        string[] pathTwoArray = new string[pathTwo.Count];
        pathTwo.CopyTo(pathTwoArray, 0);

        for (int i = 0; i < pathOneArray.Length + pathTwoArray.Length; i++)
        {
            if (i < pathOneArray.Length)
            {
                mergedPath.AddLast(pathOneArray[i]);
            }
            else
            {
                mergedPath.AddLast(pathTwoArray[i-pathOneArray.Length]);
            }
        }

        return mergedPath;
    }

    public static List<String> GetWildcardRoots(String word)
    {
        List<String> words = new List<String>();
        for (int i = 0; i < word.Length; i++)
        {
            String w = word.Substring(0, i) + "_" + word.Substring(i + 1);
            words.Add(w);
        }
        return words;
    }

    public static Dictionary<String, List<String>> GetWildcardToWordList(String[] words)
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
            Console.WriteLine(String.Join(", ", list));
        }

    }
}
