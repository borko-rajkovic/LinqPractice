<Query Kind="Program" />

public class Q2_04_Partition
{
    public class LinkedListNode
    {
        public LinkedListNode Next { get; set; }
        public LinkedListNode Prev { get; set; }
        public LinkedListNode Last { get; set; }
        public int Data { get; set; }

        public LinkedListNode(int d, LinkedListNode n, LinkedListNode p)
        {
            Data = d;
            SetNext(n);
            SetPrevious(p);
        }

        public LinkedListNode()
        { }

        public void SetNext(LinkedListNode n)
        {
            Next = n;
            if (this == Last)
            {
                Last = n;
            }
            if (n != null && n.Prev != this)
            {
                n.SetPrevious(this);
            }
        }

        public void SetPrevious(LinkedListNode p)
        {
            Prev = p;
            if (p != null && p.Next != this)
            {
                p.SetNext(this);
            }
        }

        public String PrintForward()
        {
            if (Next != null)
            {
                return string.Format("{0}->{1}", Data, Next.PrintForward());
            }
            else
            {
                return string.Format("{0}", Data);
            }
        }

        public LinkedListNode Clone()
        {
            LinkedListNode next2 = null;
            if (Next != null)
            {
                next2 = Next.Clone();
            }
            LinkedListNode head2 = new LinkedListNode(Data, next2, null);
            return head2;
        }
    }

    private static LinkedListNode Partition4(LinkedListNode listHead, int pivot)
    {
        LinkedListNode leftSubList = null;
        LinkedListNode rightSubList = null;
        LinkedListNode rightSubListHead = null;
        LinkedListNode pivotNode = null;

        var currentNode = listHead;

        while (currentNode != null)
        {
            var nextNode = currentNode.Next;
            currentNode.Next = null;

            if (currentNode.Data < pivot)
            {
                leftSubList = leftSubList == null
                    ? currentNode
                    : leftSubList = leftSubList.Next = currentNode;
            }
            else if (currentNode.Data > pivot)
            {
                rightSubList = rightSubList == null
                    ? rightSubListHead = currentNode
                    : rightSubList = rightSubList.Next = currentNode;
            }
            else
            {
                pivotNode = currentNode;
            }

            currentNode = nextNode;
        }

        pivotNode.Next = rightSubListHead;
        rightSubListHead = pivotNode;
        leftSubList.Next = rightSubListHead;

        return listHead;
    }

    public static void Main()
    {
        /* Create linked list */
        int[] vals = { 1, 3, 7, 5, 2, 9, 5, 1, 8, 5, 4 };
        var head = new LinkedListNode(vals[0], null, null);
        var current = head;

        for (var i = 1; i < vals.Length; i++)
        {
            current = new LinkedListNode(vals[i], null, current);
        }
        Console.WriteLine(head.PrintForward());
		Console.WriteLine();

        /* Partition */
        var h = Partition4(head, 5);

        /* Print Result */
        Console.WriteLine(h.PrintForward());
    }
}
