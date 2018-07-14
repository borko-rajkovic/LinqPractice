<Query Kind="Program" />

public class Program
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

    private static bool IsPalindrome(LinkedListNode head)
    {
        var fast = head;
        var slow = head;

        var stack = new Stack<int>();

        while (fast != null && fast.Next != null)
        {
            stack.Push(slow.Data);
            slow = slow.Next;
            fast = fast.Next.Next;
        }

        /* Has odd number of elements, so skip the middle */
        if (fast != null)
        {
            slow = slow.Next;
        }

        while (slow != null)
        {
            var top = stack.Pop();
            Console.WriteLine(slow.Data + " " + top);

            if (top != slow.Data)
            {
                return false;
            }
            slow = slow.Next;
        }

        return true;
    }

    public static void Main()
    {
        const int length = 10;
        var nodes = new LinkedListNode[length];

        for (var i = 0; i < length; i++)
        {
            nodes[i] = new LinkedListNode(i >= length / 2 ? length - i - 1 : i, null, null);
        }

        for (var i = 0; i < length; i++)
        {
            if (i < length - 1)
            {
                nodes[i].SetNext(nodes[i + 1]);
            }

            if (i > 0)
            {
                nodes[i].SetPrevious(nodes[i - 1]);
            }
        }
        // nodes[length - 2].data = 9; // Uncomment to ruin palindrome

        var head = nodes[0];
        Console.WriteLine(head.PrintForward());
        Console.WriteLine(IsPalindrome(head));
    }
}
