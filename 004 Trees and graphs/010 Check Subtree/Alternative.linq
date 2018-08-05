<Query Kind="Program" />

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

	public static TreeNode CreateTreeFromArray(int[] array)
	{
		if (array.Length > 0)
		{
			TreeNode root = new TreeNode(array[0]);
			LinkedList<TreeNode> linkedList = new LinkedList<TreeNode>();
			linkedList.AddLast(root);
			Boolean done = false;
			int i = 1;
			while (!done)
			{
				TreeNode r = linkedList.Last.Value;
				if (r.left == null)
				{
					r.left = new TreeNode(array[i]);
					i++;
					linkedList.AddLast(r.left);
				}
				else if (r.right == null)
				{
					r.right = new TreeNode(array[i]);
					i++;
					linkedList.AddLast(r.right);
				}
				else
				{
					linkedList.RemoveLast();
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

	public static Boolean ContainsTree(TreeNode t1, TreeNode t2)
	{
		if (t2 == null)
		{
			return true; // The empty tree is a subtree of every tree.
		}
		return SubTree(t1, t2);
	}

	/* Checks if the binary tree rooted at r1 contains the binary tree 
	 * rooted at r2 as a subtree somewhere within it.
	 */
	public static Boolean SubTree(TreeNode r1, TreeNode r2)
	{
		if (r1 == null)
		{
			return false; // big tree empty & subtree still not found.
		}
		else if (r1.data == r2.data && MatchTree(r1, r2))
		{
			return true;
		}
		return SubTree(r1.left, r2) || SubTree(r1.right, r2);
	}

	/* Checks if the binary tree rooted at r1 contains the 
	 * binary tree rooted at r2 as a subtree starting at r1.
	 */
	public static Boolean MatchTree(TreeNode r1, TreeNode r2)
	{
		if (r1 == null && r2 == null)
		{
			return true; // nothing left in the subtree
		}
		else if (r1 == null || r2 == null)
		{
			return false; // exactly tree is empty, therefore trees don't match
		}
		else if (r1.data != r2.data)
		{
			return false;  // data doesn't match
		}
		else
		{
			return MatchTree(r1.left, r2.left) && MatchTree(r1.right, r2.right);
		}
	}


	public static void Main(String[] args)
	{
		// t2 is a subtree of t1
		int[] array1 = { 1, 2, 1, 3, 1, 1, 5, 1, 2, 3 };
		int[] array2 = { 2, 3, 1 };

		TreeNode t1 = CreateTreeFromArray(array1);
		TreeNode t2 = CreateTreeFromArray(array2);

		if (ContainsTree(t1, t2))
		{
			Console.WriteLine("t2 is a subtree of t1");
		}
		else
		{
			Console.WriteLine("t2 is not a subtree of t1");
		}

		// t4 is not a subtree of t3
		int[] array3 = { 1, 2, 3 };
		TreeNode t3 = CreateTreeFromArray(array1);
		TreeNode t4 = CreateTreeFromArray(array3);

		if (ContainsTree(t3, t4))
		{
			Console.WriteLine("t4 is a subtree of t3");
		}
		else
		{
			Console.WriteLine("t4 is not a subtree of t3");
		}

	}

}
