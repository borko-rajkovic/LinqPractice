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

    public static Boolean IsPalindrome(LinkedListNode head)
    {
        LinkedListNode reversed = ReverseAndClone(head);
        return IsEqual(head, reversed);
    }

    public static LinkedListNode ReverseAndClone(LinkedListNode node)
    {
        LinkedListNode head = null;
        while (node != null)
        {
            LinkedListNode n = new LinkedListNode(node.Data, null, null); // Clone
            n.Next = head;
            head = n;
            node = node.Next;
        }
        return head;
    }

    public static Boolean IsEqual(LinkedListNode one, LinkedListNode two)
    {
        while (one != null && two != null)
        {
            if (one.Data != two.Data)
            {
                return false;
            }
            one = one.Next;
            two = two.Next;
        }
        return one == null && two == null;
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
