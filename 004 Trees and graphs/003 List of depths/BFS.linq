<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    /* One node of a binary tree. The data element stored is a single 
     * character.
     */
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
    }

    public static List<LinkedList<TreeNode>> CreateLevelLinkedList(TreeNode root)
    {
        List<LinkedList<TreeNode>> result = new List<LinkedList<TreeNode>>();

        /* "Visit" the root */
        LinkedList<TreeNode> current = new LinkedList<TreeNode>();
        if (root != null)
        {
            current.AddLast(root);
        }

        while (current.Count > 0)
        {
            result.Add(current); // Add previous level
            LinkedList<TreeNode> parents = current; // Go to next level
            current = new LinkedList<TreeNode>();
            foreach (TreeNode parent in parents)
            {
                /* Visit the children */
                if (parent.left != null)
                {
                    current.AddLast(parent.left);
                }
                if (parent.right != null)
                {
                    current.AddLast(parent.right);
                }
            }
        }

        return result;
    }

    public static void PrintResult(List<LinkedList<TreeNode>> result)
    {
        int depth = 0;
        foreach (LinkedList<TreeNode> entry in result)
        {
            Console.Write("Link list at depth " + depth + ":");
            while (entry.Count > 0)
            {
                Console.Write(" " + entry.First.Value.data);
                entry.RemoveFirst();
            }
            Console.WriteLine();
            depth++;
        }
    }

    public static TreeNode CreateTreeFromArray(int[] array)
    {
        if (array.Length > 0)
        {
            TreeNode root = new TreeNode(array[0]);
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            Boolean done = false;
            int i = 1;
            while (!done)
            {
                TreeNode r = queue.Peek();
                if (r.left == null)
                {
                    r.left = new TreeNode(array[i]);
                    i++;
                    queue.Enqueue(r.left);
                }
                else if (r.right == null)
                {
                    r.right = new TreeNode(array[i]);
                    i++;
                    queue.Enqueue(r.right);
                }
                else
                {
                    queue.Dequeue();
                }
                if (i == array.Length)
                {
                    done = true;
                }
            }
            return root;
        }
        else
        {
            return null;
        }
    }

    public static void Main(String[] args)
    {
        int[] nodes_flattened = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        TreeNode root = CreateTreeFromArray(nodes_flattened);
        List<LinkedList<TreeNode>> list = CreateLevelLinkedList(root);
        PrintResult(list);
    }
}
