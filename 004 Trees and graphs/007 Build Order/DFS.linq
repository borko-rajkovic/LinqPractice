<Query Kind="Program" />

public class Program
{
    public class Project
    {
        public enum State { COMPLETE, PARTIAL, BLANK };
        private List<Project> children = new List<Project>();
        private Dictionary<String, Project> map = new Dictionary<String, Project>();
        private String name;
        private State state = State.BLANK;

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
            }
        }

        public List<Project> GetChildren()
        {
            return children;
        }

        public State GetState()
        {
            return state;
        }

        public void SetState(State st)
        {
            state = st;
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
     * Assumes a pair is listed in “build order” (which is the reverse 
     * of dependency order). The pair (a, b) in dependencies indicates
     * that b depends on a and a must be built before a. */
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

    public static Boolean DoDFS(Project project, Stack<Project> stack)
    {
        if (project.GetState() == Project.State.PARTIAL)
        {
            return false; // Cycle
        }

        if (project.GetState() == Project.State.BLANK)
        {
            project.SetState(Project.State.PARTIAL);
            List<Project> children = project.GetChildren();
            foreach (Project child in children)
            {
                if (!DoDFS(child, stack))
                {
                    return false;
                }
            }
            project.SetState(Project.State.COMPLETE);
            stack.Push(project);
        }
        return true;
    }

    public static Stack<Project> OrderProjects(List<Project> projects)
    {
        Stack<Project> stack = new Stack<Project>();
        foreach (Project project in projects)
        {
            if (project.GetState() == Project.State.BLANK)
            {
                if (!DoDFS(project, stack))
                {
                    return null;
                }
            }
        }
        return stack;
    }

    public static String[] ConvertToStringList(Stack<Project> projects)
    {
        String[] buildOrder = new String[projects.Count];
        for (int i = 0; i < buildOrder.Length; i++)
        {
            buildOrder[i] = projects.Pop().GetName();
        }
        return buildOrder;
    }

    public static Stack<Project> FindBuildOrder(String[] projects, String[][] dependencies)
    {
        Graph graph = BuildGraph(projects, dependencies);
        return OrderProjects(graph.GetNodes());
    }

    public static String[] BuildOrderWrapper(String[] projects, String[][] dependencies)
    {
        Stack<Project> buildOrder = FindBuildOrder(projects, dependencies);
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
