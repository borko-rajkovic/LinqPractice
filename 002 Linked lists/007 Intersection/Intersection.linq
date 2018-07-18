<Query Kind="Program" />

public class Program
{
    public static LinkedListNode CreateLinkedListFromArray(int[] vals)
    {
        LinkedListNode head = new LinkedListNode(vals[0], null, null);
        LinkedListNode current = head;
        for (int i = 1; i < vals.Length; i++)
        {
            current = new LinkedListNode(vals[i], null, current);
        }
        return head;
    }

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

    public class Result
    {
        public LinkedListNode tail;
        public int size;
        public Result(LinkedListNode tail, int size)
        {
            this.tail = tail;
            this.size = size;
        }
    }

    public static Result GetTailAndSize(LinkedListNode list)
    {
        if (list == null) return null;

        int size = 1;
        LinkedListNode current = list;
        while (current.Next != null)
        {
            size++;
            current = current.Next;
        }
        return new Result(current, size);
    }

    public static LinkedListNode GetKthNode(LinkedListNode head, int k)
    {
        LinkedListNode current = head;
        while (k > 0 && current != null)
        {
            current = current.Next;
            k--;
        }
        return current;
    }

    public static LinkedListNode FindIntersection(LinkedListNode list1, LinkedListNode list2)
    {
        if (list1 == null || list2 == null) return null;

        /* Get tail and sizes. */
        Result result1 = GetTailAndSize(list1);
        Result result2 = GetTailAndSize(list2);

        /* If different tail nodes, then there's no intersection. */
        if (result1.tail != result2.tail)
        {
            return null;
        }

        /* Set pointers to the start of each linked list. */
        LinkedListNode shorter = result1.size < result2.size ? list1 : list2;
        LinkedListNode longer = result1.size < result2.size ? list2 : list1;

        /* Advance the pointer for the longer linked list by the difference in lengths. */
        longer = GetKthNode(longer, Math.Abs(result1.size - result2.size));

        /* Move both pointers until you have a collision. */
        while (shorter != longer)
        {
            shorter = shorter.Next;
            longer = longer.Next;
        }

        /* Return either one. */
        return longer;
    }

    public static void Main(String[] args)
    {
        /* Create linked list */
        int[] vals = { -1, -2, 0, 1, 2, 3, 4, 5, 6, 7, 8 };
        LinkedListNode list1 = CreateLinkedListFromArray(vals);

        int[] vals2 = { 12, 14, 15 };
        LinkedListNode list2 = CreateLinkedListFromArray(vals2);

        list2.Next.Next = list1.Next.Next.Next.Next;

        Console.WriteLine(list1.PrintForward());
        Console.WriteLine(list2.PrintForward());


        LinkedListNode intersection = FindIntersection(list1, list2);

        Console.WriteLine(intersection.PrintForward());

    }
}
