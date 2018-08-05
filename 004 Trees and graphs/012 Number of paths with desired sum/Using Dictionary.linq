<Query Kind="Program" />

/*
O(n) time complexity
O(log N) space complexity in balanced tree; O(n) in worst case
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
        return CountPathsWithSum(root, targetSum, 0, new Dictionary<int, int>());
    }

    public static int CountPathsWithSum(TreeNode node, int targetSum, int runningSum, Dictionary<int, int> pathCount)
    {
        if (node == null) return 0; // Base case

        runningSum += node.data;

        /* Count paths with sum ending at the current node. */
        int sum = runningSum - targetSum;
        int totalPaths = pathCount.ContainsKey(sum)?pathCount[sum]:0;

        /* If runningSum equals targetSum, then one additional path starts at root. Add in this path.*/
        if (runningSum == targetSum)
        {
            totalPaths++;
        }

        /* Add runningSum to pathCounts. */
        IncrementHashTable(pathCount, runningSum, 1);

        /* Count paths with sum on the left and right. */
        totalPaths += CountPathsWithSum(node.left, targetSum, runningSum, pathCount);
        totalPaths += CountPathsWithSum(node.right, targetSum, runningSum, pathCount);

        IncrementHashTable(pathCount, runningSum, -1); // Remove runningSum
        return totalPaths;
    }

    public static void IncrementHashTable(Dictionary<int, int> dictionary, int key, int delta)
    {
        int newCount = (dictionary.ContainsKey(key)?dictionary[key]:0) + delta;
        if (newCount == 0)
        { // Remove when zero to reduce space usage
            dictionary.Remove(key);
        }
        else
        {
            dictionary[key] = newCount;
        }
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
        Console.WriteLine(countPathsWithSum(root, 0));*/

        /*TreeNode root = new TreeNode(-7);
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
        Console.WriteLine(countPathsWithSum(root, -14));*/

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
