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

    public static Dictionary<DocPair, Double> ComputeSimilarities(Dictionary<int, Document> documents)
    {
        List<Document> docs = new List<Document>();
        foreach (var doc in documents)
        {
            docs.Add(doc.Value);
        }
        return ComputeSimilarities(docs);
    }

    public static Dictionary<DocPair, Double> ComputeSimilarities(List<Document> documents)
    {
        Dictionary<DocPair, Double> similarities = new Dictionary<DocPair, Double>();
        for (int i = 0; i < documents.Count; i++)
        {
            for (int j = i + 1; j < documents.Count; j++)
            {
                Document doc1 = documents[i];
                Document doc2 = documents[j];
                double sim = ComputeSimilarity(doc1, doc2);
                if (sim > 0)
                {
                    DocPair pair = new DocPair(doc1.GetId(), doc2.GetId());
                    similarities.Add(pair, sim);
                }
            }
        }
        return similarities;
    }

    public static double ComputeSimilarity(Document doc1, Document doc2)
    {
        int intersection = 0;
        HashSet<int> set1 = new HashSet<int>();
        foreach (var doc in doc1.GetWords())
        {
            set1.Add(doc);
        }

        foreach (int word in doc2.GetWords())
        {
            if (set1.Contains(word))
            {
                intersection++;
            }
        }

        double union = doc1.Count + doc2.Count - intersection;

        return intersection / union;
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
