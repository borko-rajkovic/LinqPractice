<Query Kind="Program" />

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


    class Program
    {

            public static void visit(TreeNode node)
            {
                if (node != null)
                {
                    Console.WriteLine(node.data);
                }
            }

            public static void inOrderTraversal(TreeNode node)
            {
                if (node != null)
                {
                    inOrderTraversal(node.left);
                    visit(node);
                    inOrderTraversal(node.right);
                }
            }

            public static void preOrderTraversal(TreeNode node)
            {
                if (node != null)
                {
                    visit(node);
                    preOrderTraversal(node.left);
                    preOrderTraversal(node.right);
                }
            }

            public static void postOrderTraversal(TreeNode node)
            {
                if (node != null)
                {
                    postOrderTraversal(node.left);
                    postOrderTraversal(node.right);
                    visit(node);
                }
            }

public static void levelOrderTraversal(TreeNode root){
    int i = 0;
    int h = height(root);

    for(i=1; i<=h; i++){
        printTreeLevelRec(root, i);
    }
}

static int height(TreeNode n){
    if(n==null)
        return 0;

    if(n.left==null && n.right==null)
        return 1;

    int lheight = height(n.left);
    int rheight = height(n.right);

    return Math.Max(lheight, rheight) + 1;
}

static void printTreeLevelRec(TreeNode  node, int desired){
    if(node==null)
        return;
    if (desired == 1)
        visit(node);

    printTreeLevelRec(node.left, desired-1);
    printTreeLevelRec(node.right, desired-1);
}

        public static void Main(String[] args)
        {
            int[] array = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // We needed this code for other files, so check out the code in the library
            TreeNode root = TreeNode.createMinimalBST(array);
			/*
				   	 5				
				  /     \
			     /  	 \
				2	      8				
			  /	  \		 / \
			1	   3	6   9
					\	 \	 \
					 4	  7   10
			*/
            Console.WriteLine();
            inOrderTraversal(root);
            Console.WriteLine();
            postOrderTraversal(root);            
            Console.WriteLine();
            preOrderTraversal(root);
            Console.WriteLine();			
			levelOrderTraversal(root);
			Console.WriteLine();
        }
}
