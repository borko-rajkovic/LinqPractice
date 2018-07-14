<Query Kind="Program" />

public class Program
{

    public class TreeNode
    {
        public int data;
        public TreeNode left;
        public TreeNode right;
        public TreeNode parent;
        private int size = 0;

        public TreeNode(int d)
        {
            data = d;
            size = 1;
        }

        private void SetLeftChild(TreeNode left)
        {
            this.left = left;
            if (left != null)
            {
                left.parent = this;
            }
        }

        private void SetRightChild(TreeNode right)
        {
            this.right = right;
            if (right != null)
            {
                right.parent = this;
            }
        }

        public void InsertInOrder(int d)
        {
            if (d <= data)
            {
                if (left == null)
                {
                    SetLeftChild(new TreeNode(d));
                }
                else
                {
                    left.InsertInOrder(d);
                }
            }
            else
            {
                if (right == null)
                {
                    SetRightChild(new TreeNode(d));
                }
                else
                {
                    right.InsertInOrder(d);
                }
            }
            size++;
        }

        public int Size()
        {
            return size;
        }

        public Boolean IsBST()
        {
            if (left != null)
            {
                if (data < left.data || !left.IsBST())
                {
                    return false;
                }
            }

            if (right != null)
            {
                if (data >= right.data || !right.IsBST())
                {
                    return false;
                }
            }

            return true;
        }

        public int Height()
        {
            int leftHeight = left != null ? left.Height() : 0;
            int rightHeight = right != null ? right.Height() : 0;
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        public TreeNode Find(int d)
        {
            if (d == data)
            {
                return this;
            }
            else if (d <= data)
            {
                return left != null ? left.Find(d) : null;
            }
            else if (d > data)
            {
                return right != null ? right.Find(d) : null;
            }
            return null;
        }

        private static TreeNode CreateMinimalBST(int[] arr, int start, int end)
        {
            if (end < start)
            {
                return null;
            }
            int mid = (start + end) / 2;
            TreeNode n = new TreeNode(arr[mid]);
            n.SetLeftChild(CreateMinimalBST(arr, start, mid - 1));
            n.SetRightChild(CreateMinimalBST(arr, mid + 1, end));
            return n;
        }

        public static TreeNode CreateMinimalBST(int[] array)
        {
            return CreateMinimalBST(array, 0, array.Length - 1);
        }

        public void Print()
        {
            PrintNode(this);
        }

        public static void PrintNode(TreeNode root)
        {
            int maxLevel = MaxLevel(root);

            var list = new List<TreeNode>() { root };

            PrintNodeInternal(list, 1, maxLevel);
        }

        private static int MaxLevel(TreeNode node)
        {
            if (node == null)
                return 0;

            return Math.Max(MaxLevel(node.left), MaxLevel(node.right)) + 1;
        }

        private static Boolean IsAllElementsNull(List<TreeNode> list)
        {
            foreach (Object o in list)
            {
                if (o != null)
                    return false;
            }

            return true;
        }

        private static void PrintNodeInternal(List<TreeNode> nodes, int level, int maxLevel)
        {
            if (nodes.Count == 0 || IsAllElementsNull(nodes))
                return;

            int floor = maxLevel - level;
            int endgeLines = (int)Math.Pow(2, (Math.Max(floor - 1, 0)));
            int firstSpaces = (int)Math.Pow(2, (floor)) - 1;
            int betweenSpaces = (int)Math.Pow(2, (floor + 1)) - 1;

            PrintWhitespaces(firstSpaces);

            List<TreeNode> newNodes = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                if (node != null)
                {
                    Console.Write(node.data);
                    newNodes.Add(node.left);
                    newNodes.Add(node.right);
                }
                else
                {
                    newNodes.Add(null);
                    newNodes.Add(null);
                    Console.Write(" ");
                }

                PrintWhitespaces(betweenSpaces);
            }
            Console.WriteLine();

            for (int i = 1; i <= endgeLines; i++)
            {
                for (int j = 0; j < nodes.Count; j++)
                {
                    PrintWhitespaces(firstSpaces - i);
                    if (nodes[j] == null)
                    {
                        PrintWhitespaces(endgeLines + endgeLines + i + 1);
                        continue;
                    }

                    if (nodes[j].left != null)
                        Console.Write("/");
                    else
                        PrintWhitespaces(1);

                    PrintWhitespaces(i + i - 1);

                    if (nodes[j].right != null)
                        Console.Write("\\");
                    else
                        PrintWhitespaces(1);

                    PrintWhitespaces(endgeLines + endgeLines - i);
                }

                Console.WriteLine();
            }

            PrintNodeInternal(newNodes, level + 1, maxLevel);
        }

        private static void PrintWhitespaces(int count)
        {
            for (int i = 0; i < count; i++)
                Console.Write(" ");
        }
    }

    public static TreeNode InorderSucc(TreeNode n)
    {
        if (n == null) return null;

        if (n.right != null)
        {
            return LeftMostChild(n.right);
        }
        else
        {
            TreeNode q = n;
            TreeNode x = q.parent;

            while (x != null && x.left != q)
            {
                q = x;
                x = x.parent;
            }

            return x;
        }
    }

    public static TreeNode LeftMostChild(TreeNode n)
    {
        if (n == null)
        {
            return null;
        }
        while (n.left != null)
        {
            n = n.left;
        }
        return n;
    }

    public static void Main(String[] args)
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        // We needed this code for other files, so check out the code in the library
        TreeNode root = TreeNode.CreateMinimalBST(array);
        root.Print();

        var data = InorderSucc(null)?.data.ToString()??"[Empty]";

        Console.Write("Successor of null is: ");
        Console.WriteLine(data);
        Console.Write($"Successor of {root.left.left.data} is: ");
        Console.WriteLine(InorderSucc(root.left.left).data);
        Console.Write($"Successor of {root.left.right.data} is: ");
        Console.WriteLine(InorderSucc(root.left.right).data);
        Console.Write($"Successor of {root.right.left.data} is: ");
        Console.WriteLine(InorderSucc(root.right.left).data);
        Console.Write($"Successor of {root.right.right.data} is: ");
        Console.WriteLine(InorderSucc(root.right.right).data);
        Console.Write($"Successor of {root.data} is: ");
        Console.WriteLine(InorderSucc(root).data);
        Console.Write($"Successor of {root.right.right.right.data} is: ");
        data = InorderSucc(root.right.right.right)?.data.ToString() ?? "[Empty]";
        Console.WriteLine(data);

    }

}
