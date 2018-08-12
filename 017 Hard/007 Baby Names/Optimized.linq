<Query Kind="Program" />

class Program
{
    public class GraphNode
    {
        private List<GraphNode> neighbors;
        private Dictionary<String, GraphNode> map;
        private String name;
        private int frequency;
        private Boolean visited = false;

        public GraphNode(String nm, int freq)
        {
            name = nm;
            frequency = freq;
            neighbors = new List<GraphNode>();
            map = new Dictionary<String, GraphNode>();
        }

        public String GetName()
        {
            return name;
        }

        public int GetFrequency()
        {
            return frequency;
        }

        public Boolean AddNeighbor(GraphNode node)
        {
            if (map.ContainsKey(node.GetName()))
            {
                return false;
            }
            neighbors.Add(node);
            map.Add(node.GetName(), node);
            return true;
        }

        public List<GraphNode> GetNeighbors()
        {
            return neighbors;
        }

        public Boolean IsVisited()
        {
            return visited;
        }

        public void SetIsVisited(Boolean v)
        {
            visited = v;
        }
    }

    public class Graph
    {
        private List<GraphNode> nodes;
        private Dictionary<String, GraphNode> map;

        public Graph()
        {
            map = new Dictionary<String, GraphNode>();
            nodes = new List<GraphNode>();
        }

        public Boolean HasNode(String name)
        {
            return map.ContainsKey(name);
        }

        public GraphNode CreateNode(String name, int freq)
        {
            if (map.ContainsKey(name))
            {
                return GetNode(name);
            }

            GraphNode node = new GraphNode(name, freq);
            nodes.Add(node);
            map.Add(name, node);
            return node;
        }

        private GraphNode GetNode(String name)
        {
            return map[name];
        }

        public List<GraphNode> GetNodes()
        {
            return nodes;
        }

        public void AddEdge(String startName, String endName)
        {
            GraphNode start = GetNode(startName);
            GraphNode end = GetNode(endName);
            if (start != null && end != null)
            {
                start.AddNeighbor(end);
                end.AddNeighbor(start);
            }
        }
    }

    /* Add all names to graph as nodes. */
    public static Graph ConstructGraph(Dictionary<String, int> names)
    {
        Graph graph = new Graph();
        foreach (var entry in names)
        {
            String name = entry.Key;
            int frequency = entry.Value;
            graph.CreateNode(name, frequency);
        }
        return graph;
    }

    /* Connect synonymous spellings. */
    public static void ConnectEdges(Graph graph, String[][] synonyms)
    {
        foreach (String[] entry in synonyms)
        {
            String name1 = entry[0];
            String name2 = entry[1];
            graph.AddEdge(name1, name2);
        }
    }

    /* Do depth-first search to find the total frequency of this 
     * component, and mark each node as visited.*/
    public static int GetComponentFrequency(GraphNode node)
    {
        if (node.IsVisited())
        {
            return 0;
        }
        node.SetIsVisited(true);
        int sum = node.GetFrequency();
        foreach (GraphNode child in node.GetNeighbors())
        {
            sum += GetComponentFrequency(child);
        }
        return sum;
    }

    /* Do DFS of each component. If a node has been visited before,
     * then its component has already been comAdded. */
    public static Dictionary<String, int> GetTrueFrequencies(Graph graph)
    {
        Dictionary<String, int> rootNames = new Dictionary<String, int>();
        foreach (GraphNode node in graph.GetNodes())
        {
            if (!node.IsVisited())
            {
                int frequency = GetComponentFrequency(node);
                String name = node.GetName();
                rootNames.Add(name, frequency);
            }
        }
        return rootNames;
    }

    public static Dictionary<String, int> TrulyMostPopular(Dictionary<String, int> names, String[][] synonyms)
    {
        Graph graph = ConstructGraph(names);
        ConnectEdges(graph, synonyms);
        Dictionary<String, int> rootNames = GetTrueFrequencies(graph);
        return rootNames;
    }

    public static void Main(String[] args)
    {
        Dictionary<String, int> names = new Dictionary<String, int>();

        names.Add("John", 3);
        names.Add("Jonathan", 4);
        names.Add("Johnny", 5);
        names.Add("Chris", 1);
        names.Add("Kris", 3);
        names.Add("Brian", 2);
        names.Add("Bryan", 4);
        names.Add("Carleton", 4);

        String[][] synonyms = new string[][]
            { new string[] {"John", "Jonathan"},
              new string[] {"Jonathan", "Johnny"},
              new string[] {"Chris", "Kris"},
              new string[] {"Brian", "Bryan"}};

        Dictionary<String, int> rootNames = TrulyMostPopular(names, synonyms);
        foreach (var entry in rootNames)
        {
            String name = entry.Key;
            int frequency = entry.Value;
            Console.WriteLine(name + ": " + frequency);
        }

    }
}
