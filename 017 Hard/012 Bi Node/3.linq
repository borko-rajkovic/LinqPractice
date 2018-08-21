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

    public static BiNode ConvertToCircular(BiNode root)
    {
        if (root == null)
        {
            return null;
        }

        BiNode part1 = ConvertToCircular(root.node1);
        BiNode part3 = ConvertToCircular(root.node2);

        if (part1 == null && part3 == null)
        {
            root.node1 = root;
            root.node2 = root;
            return root;
        }
        BiNode tail3 = part3 == null ? null : part3.node1;

        /* join left to root */
        if (part1 == null)
        {
            Concat(part3.node1, root);
        }
        else
        {
            Concat(part1.node1, root);
        }

        /* join right to root */
        if (part3 == null)
        {
            Concat(root, part1);
        }
        else
        {
            Concat(root, part3);
        }

        /* join right to left */
        if (part1 != null && part3 != null)
        {
            Concat(tail3, part1);
        }

        return part1 ?? root;
    }

    public static BiNode Convert(BiNode root)
    {
        BiNode head = ConvertToCircular(root);
        head.node1.node2 = null;
        head.node1 = null;
        return head;
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
        BiNode r = Convert(root);
        PrintLinkedListTree(r);
    }
}
