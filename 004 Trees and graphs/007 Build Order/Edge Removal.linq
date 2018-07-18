<Query Kind="Program" />

public class Program
{
    public class Project
    {
        private List<Project> children = new List<Project>();
        private Dictionary<String, Project> map = new Dictionary<String, Project>();
        private String name;
        private int dependencies = 0;

        public Project(String n)
        {
            name = n;
        }

        public String GetName()
        {
            return name;
        }

        public void AddNeighbor(Project node)
        {
            if (!map.ContainsKey(node.GetName()))
            {
                children.Add(node);
                map.Add(node.GetName(), node);
                node.IncrementDependencies();
            }
        }

        public void IncrementDependencies()
        {
            dependencies++;
        }

        public List<Project> GetChildren()
        {
            return children;
        }

        public void DecrementDependencies()
        {
            dependencies--;
        }

        public int GetNumberDependencies()
        {
            return dependencies;
        }
    }

    public class Graph
    {
        private List<Project> nodes = new List<Project>();
        private Dictionary<String, Project> map = new Dictionary<String, Project>();

        public Project GetOrCreateNode(String name)
        {
            if (!map.ContainsKey(name))
            {
                Project node = new Project(name);
                nodes.Add(node);
                map.Add(name, node);
            }

            return map[name];
        }

        public void AddEdge(String startName, String endName)
        {
            Project start = GetOrCreateNode(startName);
            Project end = GetOrCreateNode(endName);
            start.AddNeighbor(end);
        }

        public List<Project> GetNodes()
        {
            return nodes;
        }
    }

    /* Build the graph, adding the edge (a, b) if b is dependent on a. 
     * Assumes a pair is listed in “build order”. The pair (a, b) in 
     * dependencies indicates that b depends on a and a must be built
     * before b. */
    public static Graph BuildGraph(String[] projects, String[][] dependencies)
    {
        Graph graph = new Graph();
        foreach (String project in projects)
        {
            graph.GetOrCreateNode(project);
        }

        foreach (String[] dependency in dependencies)
        {
            String first = dependency[0];
            String second = dependency[1];
            graph.AddEdge(first, second);
        }

        return graph;
    }

    /* A helper function to insert projects with zero dependencies 
     * into the order array, starting at index offset. */
    public static int AddNonDependent(Project[] order, List<Project> projects, int offset)
    {
        foreach (Project project in projects)
        {
            if (project.GetNumberDependencies() == 0)
            {
                order[offset] = project;
                offset++;
            }
        }
        return offset;
    }

    public static Project[] OrderProjects(List<Project> projects)
    {
        Project[] order = new Project[projects.Count];

        /* Add “roots” to the build order first.*/
        int endOfList = AddNonDependent(order, projects, 0);

        int toBeProcessed = 0;
        while (toBeProcessed < order.Length)
        {
            Project current = order[toBeProcessed];

            /* We have a circular dependency since there are no remaining
             * projects with zero dependencies. */
            if (current == null)
            {
                return null;
            }

            /* Remove myself as a dependency. */
            List<Project> children = current.GetChildren();
            foreach (Project child in children)
            {
                child.DecrementDependencies();
            }

            /* Add children that have no one depending on them. */
            endOfList = AddNonDependent(order, children, endOfList);

            toBeProcessed++;
        }

        return order;
    }

    public static String[] ConvertToStringList(Project[] projects)
    {
        String[] buildOrder = new String[projects.Length];
        for (int i = 0; i < projects.Length; i++)
        {
            buildOrder[i] = projects[i].GetName();
        }
        return buildOrder;
    }

    public static Project[] FindBuildOrder(String[] projects, String[][] dependencies)
    {
        Graph graph = BuildGraph(projects, dependencies);
        return OrderProjects(graph.GetNodes());
    }

    public static String[] BuildOrderWrapper(String[] projects, String[][] dependencies)
    {
        Project[] buildOrder = FindBuildOrder(projects, dependencies);
        if (buildOrder == null) return null;
        String[] buildOrderString = ConvertToStringList(buildOrder);
        return buildOrderString;
    }

    public static void Main(String[] args)
    {
        String[] projects = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j" };
        String[][] dependencies = {
            new String[] {"a", "b"},
            new String[] {"b", "c"},
            new String[] {"a", "c"},
            new String[] {"a", "c"},
            new String[] {"d", "e"},
            new String[] {"b", "d"},
            new String[] {"e", "f"},
            new String[] {"a", "f"},
            new String[] {"h", "i"},
            new String[] {"h", "j"},
            new String[] {"i", "j"},
            new String[] {"g", "j"}};
        String[] buildOrder = BuildOrderWrapper(projects, dependencies);
        if (buildOrder == null)
        {
            Console.WriteLine("Circular Dependency.");
        }
        else
        {
            foreach (String s in buildOrder)
            {
                Console.WriteLine(s);
            }
        }

    }
}
