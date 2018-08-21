<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class BiNode
    {
        public BiNode node1;
        public BiNode node2;
        public int data;
        public BiNode(int d)
        {
            data = d;
        }
    }

    internal class NodePair
    {
        internal BiNode head;
        internal BiNode tail;

        public NodePair(BiNode head, BiNode tail)
        {
            this.head = head;
            this.tail = tail;
        }
    }

    public static NodePair Convert(BiNode root)
    {
        if (root == null)
        {
            return null;
        }

        NodePair part1 = Convert(root.node1);
        NodePair part2 = Convert(root.node2);

        if (part1 != null)
        {
            Concat(part1.tail, root);
        }

        if (part2 != null)
        {
            Concat(root, part2.head);
        }

        return new NodePair(part1 == null ? root : part1.head, part2 == null ? root : part2.tail);
    }

    public static void Concat(BiNode x, BiNode y)
    {
        x.node2 = y;
        y.node1 = x;
    }

    public static void PrintLinkedListTree(BiNode root)
    {
        for (BiNode node = root; node != null; node = node.node2)
        {
            if (node.node2 != null && node.node2.node1 != node)
            {
                Console.Write("inconsistent node: " + node);
            }
            Console.Write(node.data + "->");
        }
        Console.WriteLine();
    }

    public static BiNode CreateTree()
    {
        BiNode[] nodes = new BiNode[7];
        for (int i = 0; i < nodes.Length; i++)
        {
            nodes[i] = new BiNode(i);
        }
        nodes[4].node1 = nodes[2];
        nodes[4].node2 = nodes[5];
        nodes[2].node1 = nodes[1];
        nodes[2].node2 = nodes[3];
        nodes[5].node2 = nodes[6];
        nodes[1].node1 = nodes[0];
        return nodes[4];
    }

    public static void PrintAsTree(BiNode root, String spaces)
    {
        if (root == null)
        {
            Console.WriteLine(spaces + "- null");
            return;
        }
        Console.WriteLine(spaces + "- " + root.data);
        PrintAsTree(root.node1, spaces + "   ");
        PrintAsTree(root.node2, spaces + "   ");
    }

    public static void Main(String[] args)
    {
        BiNode root = CreateTree();
        PrintAsTree(root, "");
        NodePair n = Convert(root);
        PrintLinkedListTree(n.head);
    }
}
