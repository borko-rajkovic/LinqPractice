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
		if ((p == null) || (q == null))
		{
			return null;
		}

		TreeNode ap = p.parent;
		while (ap != null)
		{
			TreeNode aq = q.parent;
			while (aq != null)
			{
				if (aq == ap)
				{
					return aq;
				}
				aq = aq.parent;
			}
			ap = ap.parent;
		}
		return null;
	}

	public static void Main(String[] args)
	{
		int[] array = { 5, 3, 6, 1, 9, 11 };
		TreeNode root = new TreeNode(20);
		foreach (int a in array)
		{
			root.InsertInOrder(a);
		}
		TreeNode n1 = root.Find(1);
		TreeNode n9 = root.Find(9);
		TreeNode ancestor = CommonAncestor(root, n1, n9);
		Console.WriteLine(ancestor.data);
	}
}
