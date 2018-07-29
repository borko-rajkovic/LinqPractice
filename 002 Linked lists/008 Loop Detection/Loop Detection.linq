<Query Kind="Program" />

public static class Program
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

    private static LinkedListNode FindBeginning(LinkedListNode head)
    {
        var slow = head;
        var fast = head;

        // Find meeting point
        while (fast != null && fast.Next != null)
        {
            slow = slow.Next;
            fast = fast.Next.Next;

            if (slow == fast)
            {
                break;
            }
        }

        // Error check - there is no meeting point, and therefore no loop
        if (fast == null || fast.Next == null)
        {
            return null;
        }

        /* Move slow to Head. Keep fast at Meeting Point. Each are k steps
        /* from the Loop Start. If they move at the same pace, they must
         * meet at Loop Start. */
        slow = head;
        while (slow != fast)
        {
            slow = slow.Next;
            fast = fast.Next;
        }

        // Both now point to the start of the loop.
        return fast;
    }

    public static void Main(String[] args)
	{
        const int listLength = 10;
        const int k = 3;

        // Create linked list
        var nodes = new LinkedListNode[listLength];

        for (var i = 1; i <= listLength; i++)
        {
            nodes[i - 1] = new LinkedListNode(i, null, i - 1 > 0 ? nodes[i - 2] : null);
            Console.Write("{0} -> ", nodes[i - 1].Data);
        }
        Console.WriteLine();

        // Create loop;
        nodes[listLength - 1].Next = nodes[listLength - k - 1];
        Console.WriteLine("{0} -> {1}", nodes[listLength - 1].Data, nodes[listLength - k - 1].Data);

        var loop = FindBeginning(nodes[0]);

        if (loop == null)
        {
            Console.WriteLine("No Cycle.");
        }
        else
        {
            Console.WriteLine(loop.Data);
        }

    }
}
