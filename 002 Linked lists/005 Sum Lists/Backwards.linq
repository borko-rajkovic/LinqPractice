<Query Kind="Program" />

public class Q2_05_Sum_Lists
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

    private static LinkedListNode AddLists(LinkedListNode list1, LinkedListNode list2, int carry)
    {
        if (list1 == null && list2 == null && carry == 0)
        {
            return null;
        }

        var result = new LinkedListNode();
        var value = carry;

        if (list1 != null)
        {
            value += list1.Data;
        }
        if (list2 != null)
        {
            value += list2.Data;
        }

        result.Data = value % 10;

        if (list1 != null || list2 != null)
        {
            var more = AddLists(list1 == null ? null : list1.Next,
                                           list2 == null ? null : list2.Next,
                                           value >= 10 ? 1 : 0);
            result.SetNext(more);
        }

        return result;
    }

    private static int LinkedListToInt(LinkedListNode node)
    {
        int value = 0;

        if (node.Next != null)
        {
            value = 10 * LinkedListToInt(node.Next);
        }

        return value + node.Data;
    }

    public static void Main()
    {
        {
            var lA1 = new LinkedListNode(9, null, null);
            var lA2 = new LinkedListNode(9, null, lA1);
            var lA3 = new LinkedListNode(9, null, lA2);

            var lB1 = new LinkedListNode(1, null, null);
            var lB2 = new LinkedListNode(0, null, lB1);
            var lB3 = new LinkedListNode(0, null, lB2);

            var list3 = AddLists(lA1, lB1, 0);

            Console.WriteLine("  " + lA1.PrintForward());
            Console.WriteLine("+ " + lB1.PrintForward());
            Console.WriteLine("= " + list3.PrintForward());

            var l1 = LinkedListToInt(lA1);
            var l2 = LinkedListToInt(lB1);
            var l3 = LinkedListToInt(list3);

            Console.Write(l1 + " + " + l2 + " = " + l3 + "\n");
            Console.WriteLine(l1 + " + " + l2 + " = " + (l1 + l2));
        }
    }
}
