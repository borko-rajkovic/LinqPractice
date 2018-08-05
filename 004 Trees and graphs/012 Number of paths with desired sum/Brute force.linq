<Query Kind="Program" />

/*
O(n log n ) in balanced tree
O(n ^ 2) in worst case
*/

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

    public static int CountPathsWithSum(TreeNode root, int targetSum)
    {
        if (root == null) return 0;

        /* Count paths with sum starting from the root. */
        int pathsFromRoot = CountPathsWithSumFromNode(root, targetSum, 0);

        /* Try the nodes on the left and right. */
        int pathsOnLeft = CountPathsWithSum(root.left, targetSum);
        int pathsOnRight = CountPathsWithSum(root.right, targetSum);

        return pathsFromRoot + pathsOnLeft + pathsOnRight;
    }

    /* Returns the number of paths with this sum starting from this node. */
    public static int CountPathsWithSumFromNode(TreeNode node, int targetSum, int currentSum)
    {
        if (node == null) return 0;

        currentSum += node.data;

        int totalPaths = 0;
        if (currentSum == targetSum)
        { // Found a path from the root
            totalPaths++;
        }

        totalPaths += CountPathsWithSumFromNode(node.left, targetSum, currentSum); // Go left
        totalPaths += CountPathsWithSumFromNode(node.right, targetSum, currentSum); // Go right

        return totalPaths;
    }

    public static void Main(String[] args)
    {
		/*        
        TreeNode root = new TreeNode(5);
        root.left = new TreeNode(3);		
        root.right = new TreeNode(1);
        root.left.left = new TreeNode(-8);
        root.left.right = new TreeNode(8);
        root.right.left = new TreeNode(2);
        root.right.right = new TreeNode(6);	
        Console.WriteLine(CountPathsWithSum(root, 0));
		*/

        /*
		TreeNode root = new TreeNode(-7);
        root.left = new TreeNode(-7);
        root.left.right = new TreeNode(1);
        root.left.right.left = new TreeNode(2);
        root.right = new TreeNode(7);
        root.right.left = new TreeNode(3);
        root.right.right = new TreeNode(20);
        root.right.right.left = new TreeNode(0);
        root.right.right.left.left = new TreeNode(-3);
        root.right.right.left.left.right = new TreeNode(2);
        root.right.right.left.left.right.left = new TreeNode(1);
        Console.WriteLine(CountPathsWithSum(root, -14));
		*/

		TreeNode root = new TreeNode(0);
        root.left = new TreeNode(0);
        root.right = new TreeNode(0);
        root.right.left = new TreeNode(0);
        root.right.left.right = new TreeNode(0);
        root.right.right = new TreeNode(0);
        Console.WriteLine(CountPathsWithSum(root, 0));
        Console.WriteLine(CountPathsWithSum(root, 4));
    }

}
