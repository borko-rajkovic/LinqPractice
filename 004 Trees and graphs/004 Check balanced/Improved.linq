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

        private void setLeftChild(TreeNode left)
        {
            this.left = left;
            if (left != null)
            {
                left.parent = this;
            }
        }

        private void setRightChild(TreeNode right)
        {
            this.right = right;
            if (right != null)
            {
                right.parent = this;
            }
        }

        public void insertInOrder(int d)
        {
            if (d <= data)
            {
                if (left == null)
                {
                    setLeftChild(new TreeNode(d));
                }
                else
                {
                    left.insertInOrder(d);
                }
            }
            else
            {
                if (right == null)
                {
                    setRightChild(new TreeNode(d));
                }
                else
                {
                    right.insertInOrder(d);
                }
            }
            size++;
        }

        public int Size()
        {
            return size;
        }

        public Boolean isBST()
        {
            if (left != null)
            {
                if (data < left.data || !left.isBST())
                {
                    return false;
                }
            }

            if (right != null)
            {
                if (data >= right.data || !right.isBST())
                {
                    return false;
                }
            }

            return true;
        }

        public int height()
        {
            int leftHeight = left != null ? left.height() : 0;
            int rightHeight = right != null ? right.height() : 0;
            return 1 + Math.Max(leftHeight, rightHeight);
        }

        public TreeNode find(int d)
        {
            if (d == data)
            {
                return this;
            }
            else if (d <= data)
            {
                return left != null ? left.find(d) : null;
            }
            else if (d > data)
            {
                return right != null ? right.find(d) : null;
            }
            return null;
        }

        private static TreeNode createMinimalBST(int[] arr, int start, int end)
        {
            if (end < start)
            {
                return null;
            }
            int mid = (start + end) / 2;
            TreeNode n = new TreeNode(arr[mid]);
            n.setLeftChild(createMinimalBST(arr, start, mid - 1));
            n.setRightChild(createMinimalBST(arr, mid + 1, end));
            return n;
        }

        public static TreeNode createMinimalBST(int[] array)
        {
            return createMinimalBST(array, 0, array.Length - 1);
        }

    }


    public static int CheckHeight(TreeNode root)
    {
        if (root == null)
        {
            return -1;
        }
        int leftHeight = CheckHeight(root.left);
        if (leftHeight == int.MinValue) return int.MinValue; // Propagate error up

        int rightHeight = CheckHeight(root.right);
        if (rightHeight == int.MinValue) return int.MinValue; // Propagate error up

        int heightDiff = leftHeight - rightHeight;
        if (Math.Abs(heightDiff) > 1)
        {
            return int.MinValue; // Found error -> pass it back
        }
        else
        {
            return Math.Max(leftHeight, rightHeight) + 1;
        }
    }

    public static Boolean IsBalanced(TreeNode root)
    {
        return CheckHeight(root) != int.MinValue;
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

    public static void Main()
    {
        // Create balanced tree
        int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        TreeNode root = TreeNode.createMinimalBST(array);
        Console.WriteLine("Root? " + root.data);
        Console.WriteLine("Is balanced? " + IsBalanced(root));

        // Could be balanced, actually, but it's very unlikely...
        TreeNode unbalanced = new TreeNode(10);
        for (int i = 0; i < 10; i++)
        {
            unbalanced.insertInOrder(RandomIntInRange(0, 100));
        }
        Console.WriteLine("Root? " + unbalanced.data);
        Console.WriteLine("Is balanced? " + IsBalanced(unbalanced));

    }
}
