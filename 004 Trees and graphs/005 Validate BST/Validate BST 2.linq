<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
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
                return left?.Find(d);
            }
            else if (d > data)
            {
                return right?.Find(d);
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

    public class IntWrapper
    {
        public IntWrapper(int m)
        {
            data = m;
        }
        public int data;
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static TreeNode RandomBST(int N, int min, int max)
    {
        int d = RandomIntInRange(min, max);
        TreeNode root = new TreeNode(d);
        for (int i = 1; i < N; i++)
        {
            root.InsertInOrder(RandomIntInRange(min, max));
        }
        return root;
    }



    public static Boolean CheckBST(TreeNode n, int? min, int? max)
    {
        if (n == null)
        {
            return true;
        }
        if ((min != null && n.data <= min) || (max != null && n.data > max))
        {
            return false;
        }
        if (!CheckBST(n.left, min, n.data) ||
            !CheckBST(n.right, n.data, max))
        {
            return false;
        }
        return true;
    }

    public static Boolean CheckBST(TreeNode n)
    {
        return CheckBST(n, null, null);
    }

    public static Boolean CheckBSTAlternate(TreeNode n)
    {
        return CheckBSTAlternate(n, new IntWrapper(0), new IntWrapper(0));
    }

    public static Boolean CheckBSTAlternate(TreeNode n, IntWrapper min, IntWrapper max)
    {
        /* An alternate, less clean approach. This is not provided in the book, but is used to test the other method. */
        if (n.left == null)
        {
            min.data = n.data;
        }
        else
        {
            IntWrapper leftMin = new IntWrapper(0);
            IntWrapper leftMax = new IntWrapper(0);
            if (!CheckBSTAlternate(n.left, leftMin, leftMax))
            {
                return false;
            }
            if (leftMax.data > n.data)
            {
                return false;
            }
            min.data = leftMin.data;
        }
        if (n.right == null)
        {
            max.data = n.data;
        }
        else
        {
            IntWrapper rightMin = new IntWrapper(0);
            IntWrapper rightMax = new IntWrapper(0);
            if (!CheckBSTAlternate(n.right, rightMin, rightMax))
            {
                return false;
            }
            if (rightMin.data <= n.data)
            {
                return false;
            }
            max.data = rightMax.data;
        }
        return true;
    }

    /* Create a tree that may or may not be a BST */
    public static TreeNode CreateTestTree()
    {
        /* Create a random BST */
        TreeNode head = RandomBST(10, -10, 10);

        /* Insert an element into the BST and potentially ruin the BST property */
        TreeNode node = head;
        do
        {
            int n = RandomIntInRange(-10, 10);
            int rand = RandomIntInRange(0, 5);
            if (rand == 0)
            {
                node.data = n;
            }
            else if (rand == 1)
            {
                node = node.left;
            }
            else if (rand == 2)
            {
                node = node.right;
            }
            else if (rand == 3 || rand == 4)
            {
                break;
            }
        } while (node != null);

        return head;
    }

    public static void Main(String[] args)
    {
        /* Simple test -- create one */
        int[] array = { int.MinValue, 3, 5, 6, 10, 13, 15, int.MaxValue };
        TreeNode node = TreeNode.CreateMinimalBST(array);
        //node.left.data = 6; // "ruin" the BST property by changing one of the elements
        node.Print();
        Boolean isBst = CheckBST(node);
        Console.WriteLine(isBst);

        /* More elaborate test -- creates 100 trees (some BST, some not) and compares the outputs of various methods. */
        /*
		for (int i = 0; i < 100; i++) {
            TreeNode head = CreateTestTree();

            // Compare results 
            Boolean isBst1 = CheckBST(head);
            Boolean isBst2 = CheckBSTAlternate(head);

            if (isBst1 != isBst2) {
                Console.WriteLine("*********************** ERROR *******************");
                head.Print();
                break;
            } else {
                Console.WriteLine(isBst1 + " | " + isBst2);
                head.Print();
            }
        }
		*/
    }
}
