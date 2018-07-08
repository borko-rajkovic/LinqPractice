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

    private static LinkedListNode Partition3(LinkedListNode listHead, int pivot)
    {
        var leftList = new LinkedListNode(); // empty temp node to not have an IF inside the loop
        var rightList = new LinkedListNode(pivot, null, null);

        var leftListHead = leftList; // Used at the end to remove the empty node.
        var rightListHead = rightList; // Used at the end to merge lists.

        var currentNode = listHead;

        while (currentNode != null)
        {
            if (currentNode.Data < pivot)
            {
                leftList = new LinkedListNode(currentNode.Data, null, leftList);
            }
            else if (currentNode.Data > pivot)
            {
                rightList = new LinkedListNode(currentNode.Data, null, rightList);
            }

            currentNode = currentNode.Next;
        }

        leftList.Next = rightListHead;

        var finalList = leftListHead.Next;
        leftListHead.Next = null; // remove the temp node, GC will release the mem

        return finalList;
    }

    public static void Main()
    {
        /* Create linked list */
        int[] vals = { 1, 3, 7, 5, 2, 9, 4 };
        var head = new LinkedListNode(vals[0], null, null);
        var current = head;

        for (var i = 1; i < vals.Length; i++)
        {
            current = new LinkedListNode(vals[i], null, current);
        }
        Console.WriteLine(head.PrintForward());
		Console.WriteLine();

        /* Partition */
        var h = Partition3(head, 5);

        /* Print Result */
        Console.WriteLine(h.PrintForward());
    }
}
