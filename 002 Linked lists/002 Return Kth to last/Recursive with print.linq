<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

public class LinkedListNode
{
    public LinkedListNode Next { get; set; }
    public LinkedListNode Prev { get; set; }
    public LinkedListNode Last { get; set; }
    public int Data { get; set; }

    public static LinkedListNode RandomLinkedList(int N, int min, int max)
    {
        LinkedListNode root = new LinkedListNode(RandomIntInRange(min, max), null, null);
        LinkedListNode prev = root;
        for (int i = 1; i < N; i++)
        {
            int data = RandomIntInRange(min, max);
            LinkedListNode next = new LinkedListNode(data, null, null);
            prev.SetNext(next);
            prev = next;
        }
        return root;
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    private static readonly Random RandomIntNumbers = new Random();

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

public class Program
{
    private static int NthToLastR1(LinkedListNode head, int n)
    {
        if (n == 0 || head == null)
        {
            return 0;
        }

        var k = NthToLastR1(head.Next, n) + 1;

        if (k == n)
        {
            Console.WriteLine(n + "th to last node is " + head.Data);
        }

        return k;
    }

    static void Main()
    {
        var head = LinkedListNode.RandomLinkedList(10, 0, 10);
        Console.WriteLine(head.PrintForward());
        const int nth = 3;

        NthToLastR1(head, nth);
    }
}
