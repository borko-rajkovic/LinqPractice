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

	public static TreeNode CommonAncestorBad(TreeNode root, TreeNode p, TreeNode q)
	{
		if (root == null)
		{
			return null;
		}
		if (root == p && root == q)
		{
			return root;
		}

		TreeNode x = CommonAncestorBad(root.left, p, q);
		if (x != null && x != p && x != q)
		{ // Found common ancestor
			return x;
		}

		TreeNode y = CommonAncestorBad(root.right, p, q);
		if (y != null && y != p && y != q)
		{
			return y;
		}

		if (x != null && y != null)
		{
			return root; // This is the common ancestor
		}
		else if (root == p || root == q)
		{
			return root;
		}
		else
		{
			return x == null ? y : x;
		}
	}

	public static void Main(String[] args)
	{
		int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
		TreeNode root = TreeNode.CreateMinimalBST(array);
		TreeNode n3 = root.Find(9);
		TreeNode n7 = new TreeNode(6);//root.find(10);
		TreeNode ancestor = CommonAncestorBad(root, n3, n7);
		if (ancestor != null)
		{
			Console.WriteLine(ancestor.data);
		}
		else
		{
			Console.WriteLine("null");
		}
	}
}
