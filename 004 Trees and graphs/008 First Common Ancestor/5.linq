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

    public static TreeNode CommonAncestor(TreeNode root, TreeNode p, TreeNode q)
    {
        if (!Covers(root, p) || !Covers(root, q))
        { // Error check - one node is not in tree
            return null;
        }
        return AncestorHelper(root, p, q);
    }

    public static TreeNode AncestorHelper(TreeNode root, TreeNode p, TreeNode q)
    {
        if (root == null || root == p || root == q)
        {
            return root;
        }

        Boolean pIsOnLeft = Covers(root.left, p);
        Boolean qIsOnLeft = Covers(root.left, q);
        if (pIsOnLeft != qIsOnLeft)
        { // Nodes are on different side
            return root;
        }
        TreeNode childSide = pIsOnLeft ? root.left : root.right;
        return AncestorHelper(childSide, p, q);
    }

    public static Boolean Covers(TreeNode root, TreeNode p)
    {
        if (root == null) return false;
        if (root == p) return true;
        return Covers(root.left, p) || Covers(root.right, p);
    }

    public static void Main(String[] args)
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        TreeNode root = TreeNode.CreateMinimalBST(array);
        TreeNode n3 = root.Find(1);
        TreeNode n7 = root.Find(7);
        TreeNode ancestor = CommonAncestor(root, n3, n7);
        Console.WriteLine(ancestor.data);
    }
}
