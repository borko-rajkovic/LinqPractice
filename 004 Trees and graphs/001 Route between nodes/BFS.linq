<Query Kind="Program" />

    public enum State
    {
        Unvisited, Visited, Visiting
    }

    class Node
    {
        private Node[] adjacent;
        public int adjacentCount;
        private String vertex;
        public State state;
        public Node(String vertex, int adjacentLength)
        {
            this.vertex = vertex;
            adjacentCount = 0;
            adjacent = new Node[adjacentLength];
        }

        public void addAdjacent(Node x)
        {
            if (adjacentCount < adjacent.Length)
            {
                this.adjacent[adjacentCount] = x;
                adjacentCount++;
            }
            else
            {
                Console.WriteLine("No more adjacent can be added");
            }
        }
        public Node[] getAdjacent()
        {
            return adjacent;
        }
        public String getVertex()
        {
            return vertex;
        }
    }

    class Program
    {
        public class Graph
        {
            public static int MAX_VERTICES = 6;
            private Node[] vertices;
            public int count;
            public Graph()
            {
                vertices = new Node[MAX_VERTICES];
                count = 0;
            }

            public void addNode(Node x)
            {
                if (count < vertices.Length)
                {
                    vertices[count] = x;
                    count++;
                }
                else
                {
                    Console.WriteLine("Graph full");
                }
            }

            public Node[] getNodes()
            {
                return vertices;
            }
        }

        public static Graph createNewGraph()
        {
            Graph g = new Graph();
            Node[] temp = new Node[6];

            temp[0] = new Node("a", 3);
            temp[1] = new Node("b", 0);
            temp[2] = new Node("c", 0);
            temp[3] = new Node("d", 1);
            temp[4] = new Node("e", 1);
            temp[5] = new Node("f", 0);

            temp[0].addAdjacent(temp[1]);
            temp[0].addAdjacent(temp[2]);
            temp[0].addAdjacent(temp[3]);
            temp[3].addAdjacent(temp[4]);
            temp[4].addAdjacent(temp[5]);
            for (int i = 0; i < 6; i++)
            {
                g.addNode(temp[i]);
            }
            return g;
        }



        public static void Main()
        {
            Graph g = createNewGraph();
            Node[] n = g.getNodes();
            Node start = n[3];
            Node end = n[5];
            Console.WriteLine(search(g, start, end));
        }

        public static Boolean search(Graph g, Node start, Node end)
        {
            Queue<Node> q = new Queue<Node>();
            foreach (Node node in g.getNodes())
            {
                node.state = State.Unvisited;
            }
            start.state = State.Visiting;
            q.Enqueue(start);
            Node u;
            while (q.Count!=0)
            {
                u = q.Dequeue();
                if (u != null)
                {
                    foreach (Node v in u.getAdjacent())
                    {
                        if (v.state == State.Unvisited)
                        {
                            if (v == end)
                            {
                                return true;
                            }
                            else
                            {
                                v.state = State.Visiting;
                                q.Enqueue(v);
                            }
                        }
                    }
                    u.state = State.Visited;
                }
            }
            return false;
        }


}