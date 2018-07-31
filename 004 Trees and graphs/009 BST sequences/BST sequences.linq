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

    public static void WeaveLists(LinkedList<int> first, LinkedList<int> second, List<LinkedList<int>> results, LinkedList<int> prefix)
    {
        /* One list is empty. Add the remainder to [a cloned] prefix and
         * store result. */
        if (first.Count == 0 || second.Count == 0)
        {
            LinkedList<int> result = new LinkedList<int>();
            foreach (var item in prefix)
            {
                result.AddLast(item);
            }
            foreach (var item in first)
            {
                result.AddLast(item);
            }
            foreach (var item in second)
            {
                result.AddLast(item);
            }
            results.Add(result);
            return;
        }

        /* Recurse with head of first added to the prefix. Removing the
         * head will damage first, so we’ll need to put it back where we
         * found it afterwards. */
        int headFirst = first.First.Value;
        first.RemoveFirst();
        prefix.AddLast(headFirst);
        WeaveLists(first, second, results, prefix);
        prefix.RemoveLast();
        first.AddFirst(headFirst);

        /* Do the same thing with second, damaging and then restoring
         * the list.*/
        int headSecond = second.First.Value;
        second.RemoveFirst();
        prefix.AddLast(headSecond);
        WeaveLists(first, second, results, prefix);
        prefix.RemoveLast();
        second.AddFirst(headSecond);
    }

    public static List<LinkedList<int>> AllSequences(TreeNode node)
    {
        List<LinkedList<int>> result = new List<LinkedList<int>>();

        if (node == null)
        {
            result.Add(new LinkedList<int>());
            return result;
        }

        LinkedList<int> prefix = new LinkedList<int>();
        prefix.AddLast(node.data);

        /* Recurse on left and right subtrees. */
        List<LinkedList<int>> leftSeq = AllSequences(node.left);
        List<LinkedList<int>> rightSeq = AllSequences(node.right);

        /* Weave together each list from the left and right sides. */
        foreach (LinkedList<int> left in leftSeq)
        {
            foreach (LinkedList<int> right in rightSeq)
            {
                List<LinkedList<int>> weaved = new List<LinkedList<int>>();
                WeaveLists(left, right, weaved, prefix);
                result.AddRange(weaved);
            }
        }
        return result;
    }

    public static void Main(String[] args)
    {
        TreeNode node = new TreeNode(100);
        int[] array = { 100, 50, 20, 75, 150, 120, 170 };
        foreach (int a in array)
        {
            node.InsertInOrder(a);
        }
        List<LinkedList<int>> allSeq = AllSequences(node);
        foreach (LinkedList<int> list in allSeq)
        {
            int i = 0;
            int[] ints = new int[list.Count];
            foreach (var item in list)
            {
                ints[i++] = item;
            }
            Console.WriteLine($"[{string.Join(", ", ints)}]");
        }
        Console.WriteLine(allSeq.Count);

    }
}
