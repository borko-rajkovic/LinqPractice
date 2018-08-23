<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class TrieNode
    {
        private Dictionary<Char, TrieNode> children;
        private List<int> indexes;

        public TrieNode()
        {
            children = new Dictionary<Char, TrieNode>();
            indexes = new List<int>();
        }

        public void InsertString(String s, int index)
        {
            if (s == null) return;
            indexes.Add(index);
            if (s.Length > 0)
            {
                char value = s[0];
                TrieNode child = null;
                if (children.ContainsKey(value))
                {
                    child = children[value];
                }
                else
                {
                    child = new TrieNode();
                    children.Add(value, child);
                }
                String remainder = s.Substring(1);
                child.InsertString(remainder, index + 1);
            }
            else
            {
                children.Add('\0', null);
            }
        }

        public List<int> Search(String s)
        {
            if (s == null || s.Length == 0)
            {
                return indexes;
            }
            else
            {
                char first = s[0];
                if (children.ContainsKey(first))
                {
                    String remainder = s.Substring(1);
                    return children[first].Search(remainder);
                }
            }
            return null;
        }

        public Boolean Terminates()
        {
            return children.ContainsKey('\0');
        }

        public TrieNode GetChild(char c)
        {
            return children[c];
        }
    }

    public class Trie
    {
        private TrieNode root = new TrieNode();

        public List<int> Search(String s)
        {
            return root.Search(s);
        }

        public void InsertString(String str, int location)
        {
            root.InsertString(str, location);
        }

        public TrieNode GetRoot()
        {
            return root;
        }
    }

    public static void SubtractValue(List<int> locations, int delta)
    {
        if (locations == null) return;
        for (int i = 0; i < locations.Count; i++)
        {
            locations[i] = locations[i] - delta;
        }
    }

    public static Trie CreateTrieFromString(String s)
    {
        Trie trie = new Trie();
        for (int i = 0; i < s.Length; i++)
        {
            String suffix = s.Substring(i);
            trie.InsertString(suffix, i);
        }
        return trie;
    }

    public static Dictionary<String, List<int>> SearchAll(String big, String[] smalls)
    {
        Dictionary<String, List<int>> lookup = new Dictionary<String, List<int>>();
        Trie tree = CreateTrieFromString(big);
        foreach (String s in smalls)
        {
            /* Get terminating location of each occurrence.*/
            List<int> locations = tree.Search(s);

            /* Adjust to starting location. */
            SubtractValue(locations, s.Length);

            /* Insert. */
            lookup.Add(s, locations);
        }

        return lookup;
    }

    public static void Main(String[] args)
    {
        String big = "mississippi";
        String[] smalls = { "is", "ppi", "hi", "sis", "i", "mississippi" };
        Dictionary<String, List<int>> locations = SearchAll(big, smalls);
        foreach (var location in locations)
        {
            Console.Write(location.Key + ": ");
            if (location.Value != null) Console.Write(string.Join(", ", location.Value));
            Console.WriteLine();
        }
    }
}
