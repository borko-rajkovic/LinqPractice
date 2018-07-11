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

    private class PartialSum
    {
        public LinkedListNode Sum = null;
        public int Carry = 0;
    }

    private static int Length(LinkedListNode l)
    {
        if (l == null)
        {
            return 0;
        }
        else
        {
            return 1 + Length(l.Next);
        }
    }

    private static PartialSum AddListsHelper(LinkedListNode list1, LinkedListNode list2)
    {
        if (list1 == null && list2 == null)
        {
            return new PartialSum();
        }

        var sum = new PartialSum();
        var val = 0;

        if (list1 != null)
        {
            sum = AddListsHelper(list1.Next, list2.Next);
            val = sum.Carry + list1.Data + list2.Data;
        }

        var fullResult = insertBefore(sum.Sum, val % 10);
        sum.Sum = fullResult;
        sum.Carry = val / 10;

        return sum;
    }

    private static LinkedListNode AddLists2(LinkedListNode list1, LinkedListNode list2)
    {
        var len1 = Length(list1);
        var len2 = Length(list2);

        if (len1 < len2)
        {
            list1 = PadList(list1, len2 - len1);
        }
        else
        {
            list2 = PadList(list2, len1 - len2);
        }

        var sum = AddListsHelper(list1, list2);

        if (sum.Carry == 0)
        {
            return sum.Sum;
        }
        else
        {
            var result = insertBefore(sum.Sum, sum.Carry);
            return result;
        }
    }

    private static LinkedListNode PadList(LinkedListNode listNode, int padding)
    {
        var head = listNode;

        for (var i = 0; i < padding; i++)
        {
            var n = new LinkedListNode(0, null, null);
            head.Prev = n;
            n.Next = head;
            head = n;
        }

        return head;
    }

    private static LinkedListNode insertBefore(LinkedListNode list, int data)
    {
        var node = new LinkedListNode(data, null, null);

        if (list != null)
        {
            list.Prev = node;
            node.Next = list;
        }

        return node;
    }

    private static int linkedListToInt(LinkedListNode node)
    {
        int value = 0;

        while (node != null)
        {
            value = value * 10 + node.Data;
            node = node.Next;
        }

        return value;
    }

    public static void Main()
    {
        {
            var lA1 = new LinkedListNode(3, null, null);
            var lA2 = new LinkedListNode(1, null, lA1);
            //LinkedListNode lA3 = new LinkedListNode(5, null, lA2);

            var lB1 = new LinkedListNode(5, null, null);
            var lB2 = new LinkedListNode(9, null, lB1);
            var lB3 = new LinkedListNode(1, null, lB2);

            var list3 = AddLists2(lA1, lB1);

			Console.WriteLine("  " + lA1.PrintForward());
            Console.WriteLine("+ " + lB1.PrintForward());
            Console.WriteLine("= " + list3.PrintForward());

            var l1 = linkedListToInt(lA1);
            var l2 = linkedListToInt(lB1);
            var l3 = linkedListToInt(list3);

            Console.Write(l1 + " + " + l2 + " = " + l3 + "\n");
            Console.WriteLine(l1 + " + " + l2 + " = " + (l1 + l2));
        }
    }
}
