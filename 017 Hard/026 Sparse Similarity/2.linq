<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class DocPair
    {
        public int doc1;
        public int doc2;

        public DocPair(int d1, int d2)
        {
            doc1 = d1;
            doc2 = d2;
        }

        public override Boolean Equals(Object o)
        {
            if (o is DocPair)
            {
                DocPair p = (DocPair)o;
                return p.doc1 == doc1 && p.doc2 == doc2;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (doc1 * 31) ^ doc2;
        }
    }

    public class Document
    {
        private List<int> words;
        private int docId;

        public Document(int id, List<int> w)
        {
            docId = id;
            words = w;
        }

        public List<int> GetWords()
        {
            return words;
        }

        public int GetId()
        {
            return docId;
        }

        public int Count
        {
            get
            {
                return words == null ? 0 : words.Count;
            }
        }
    }

    public static List<int> RemoveDups(int[] array)
    {
        HashSet<int> set = new HashSet<int>();
        foreach (int a in array)
        {
            set.Add(a);
        }
        List<int> list = new List<int>();
        list.AddRange(set);
        return list;
    }

    public static void PrintSim(Dictionary<DocPair, Double> similarities)
    {
        foreach (var result in similarities)
        {
            DocPair pair = result.Key;
            Double sim = result.Value;
            Console.WriteLine(pair.doc1 + ", " + pair.doc2 + " : " + sim);
        }
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int[] RandomArray(int N, int min, int max)
    {
        int[] array = new int[N];
        for (int j = 0; j < N; j++)
        {
            array[j] = RandomIntInRange(min, max);
        }
        return array;
    }

    public static Dictionary<DocPair, Double> ComputeSimilarities(Dictionary<int, Document> documents)
    {
        Dictionary<int, List<int>> wordToDocs = GroupWords(documents);
        Dictionary<DocPair, Double> similarities = ComputeIntersections(wordToDocs);        
        return AdjustToSimilarities(documents, similarities);
    }

    /* Create hash table from each word to where it appears. */
    public static Dictionary<int, List<int>> GroupWords(Dictionary<int, Document> documents)
    {
        Dictionary<int, List<int>> wordToDocs = new Dictionary<int, List<int>>();

        foreach (Document doc in documents.Values)
        {
            List<int> words = doc.GetWords();
            foreach (int word in words)
            {
                if (!wordToDocs.ContainsKey(word)) wordToDocs.Add(word, new List<int>());
                wordToDocs[word].Add(doc.GetId());
            }
        }

        return wordToDocs;
    }

    /* Compute intersections of documents. Iterate through each list of 
     * documents and then each pair within that list, incrementing the 
     * intersection of each page. */
    public static Dictionary<DocPair, Double> ComputeIntersections(Dictionary<int, List<int>> wordToDocs)
    {
        Dictionary<DocPair, Double> similarities = new Dictionary<DocPair, Double>();
        foreach (var word in wordToDocs)
        {
            List<int> docs = wordToDocs[word.Key];
            docs.Sort();
            for (int i = 0; i < docs.Count; i++)
            {
                for (int j = i + 1; j < docs.Count; j++)
                {
                    Increment(similarities, docs[i], docs[j]);
                }
            }
        }

        return similarities;
    }

    /* Increment the intersection size of each document pair. */
    public static void Increment(Dictionary<DocPair, Double> similarities, int doc1, int doc2)
    {
        DocPair pair = new DocPair(doc1, doc2);
        if (!similarities.ContainsKey(pair))
        {
            similarities.Add(pair, 1.0);
        }
        else
        {
            similarities[pair] = similarities[pair] + 1;
        }
    }

    /* Adjust the intersection value to become the similarity. */
    public static Dictionary<DocPair, Double> AdjustToSimilarities(Dictionary<int, Document> documents, Dictionary<DocPair, Double> similarities)
    {
        Dictionary<DocPair, Double> newSimilarities = new Dictionary<DocPair, double>();

        foreach (var entry in similarities)
        {
            DocPair pair = entry.Key;
            Double intersection = entry.Value;
            Document doc1 = documents[pair.doc1];
            Document doc2 = documents[pair.doc2];
            double union = (double)doc1.Count + doc2.Count - intersection;
            if (!newSimilarities.ContainsKey(entry.Key)) newSimilarities.Add(entry.Key, 0);
            newSimilarities[entry.Key] = (double)intersection / (double)union;
        }

        return newSimilarities;
    }

    public static void Main(String[] args)
    {
        int[][] tempArray =
        {
            new int[] { 3, 5, 1, 9 },
            new int[] { 2, 10, 0, 6},
            new int[] { 3, 4, 7, 9, 6},
            new int[] { 0, 4},
            new int[] { 7, 4, 8, 5},
            new int[] { 2, 3, 0, 10},
            new int[] { 0, 7, 2, 4, 10},
            new int[] { 1, 10, 0},
            new int[] { 3, 0, 5, 10, 9},
            new int[] { 3, 6, 7, 10 }
        };
        int numDocuments = 10;
        Dictionary<int, Document> documents = new Dictionary<int, Document>();
        for (int i = 0; i < numDocuments; i++)
        {
            int[] words = tempArray[i];
            List<int> w = RemoveDups(words);
            Console.WriteLine(i + ": " + String.Join(", ", w));
            Document doc = new Document(i, w);
            documents.Add(i, doc);
        }

        Dictionary<DocPair, Double> similarities = ComputeSimilarities(documents);
        PrintSim(similarities);

    }
}
