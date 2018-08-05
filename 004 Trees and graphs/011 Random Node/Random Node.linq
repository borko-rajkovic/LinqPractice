<Query Kind="Program" />

/*
Other solutions:

1) O(n) space; O(n) time - Copy all nodes to array and return random.
2) O(n) space; O(n) time - Keep array of all nodes, but deletion will take O(n) time
3) O(n) space; O(n) time - Keep array of all nodes with indexes and use binary search tree search for searching random node. Insert/delete still takes O(n) time
4) Not working - Taking depth of the node tree and traversing randomly. It makes unequal probabilities for each node (level with fewer nodes has bigger probability).
5) Not working - With probability of 1/3 take current node or traverse left and traverse right. Unequal probabilities.
6) O(log N) in balanced trees, or O(D) where D is max depth of the tree - Take root with 1/N probability and traverse left with LEFT_SIZE/N and right with RIGHT_SIZE/N probabilities.

*/


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
		private int size = 0;

		public TreeNode(int d)
		{
			data = d;
			size = 1;
		}

		public void InsertInOrder(int d)
		{
			if (d <= data)
			{
				if (left == null)
				{
					left = new TreeNode(d);
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
					right = new TreeNode(d);
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

		public TreeNode GetRandomNode()
		{
			int leftSize = left == null ? 0 : left.Size();
			Random random = new Random();
			int index = random.Next(size);
			if (index < leftSize)
			{
				return left.GetRandomNode();
			}
			else if (index == leftSize)
			{
				return this;
			}
			else
			{
				return right.GetRandomNode();
			}
		}

		public TreeNode GetIthNode(int i)
		{
			int leftSize = left == null ? 0 : left.Size();
			if (i < leftSize)
			{
				return left.GetIthNode(i);
			}
			else if (i == leftSize)
			{
				return this;
			}
			else
			{
				return right.GetIthNode(i - (leftSize + 1));
			}
		}
	}

	public class Tree
	{
		TreeNode root = null;

		public void InsertInOrder(int value)
		{
			if (root == null)
			{
				root = new TreeNode(value);
			}
			else
			{
				root.InsertInOrder(value);
			}
		}

		public int Size()
		{
			return root == null ? 0 : root.Size();
		}

		public TreeNode GetRandomNode()
		{
			if (root == null) return null;

			Random random = new Random();
			int i = random.Next(Size());
			return root.GetIthNode(i);
		}
	}


	public static void Main(String[] args)
	{
		int[] counts = new int[10];
		for (int i = 0; i < 1000000; i++)
		{
			Tree tree = new Tree();
			int[] array = { 1, 0, 6, 2, 3, 9, 4, 5, 8, 7 };
			foreach (int x in array)
			{
				tree.InsertInOrder(x);
			}
			int d = tree.GetRandomNode().data;
			counts[d]++;
		}

		for (int i = 0; i < counts.Length; i++)
		{
			Console.WriteLine(i + ": " + counts[i]);
		}

	}

}
