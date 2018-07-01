<Query Kind="Program" />

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

        private static void DeleteDupsA(LinkedListNode node)
        {
			//Keeping dictionary of integers so we can skip those who are duplicates
            var table = new Dictionary<int, bool>();
            LinkedListNode previous = null;

            while (node != null)
            {
                if (table.ContainsKey(node.Data))
                {
                    if (previous != null)
                    {
                        previous.Next = node.Next;
                    }
                }
                else
                {
                    table.Add(node.Data, true);
                    previous = node;
                }

                node = node.Next;
            }
        }

        public static void Main()
        {
            var first = new LinkedListNode(0, null, null);
            var originalList = first;
            var second = first;

            for (var i = 1; i < 8; i++)
            {
                second = new LinkedListNode(i % 4, null, null);
                first.SetNext(second);
                second.SetPrevious(first);
                first = second;
            }

            var list1 = originalList.Clone();

            DeleteDupsA(list1);

            Console.WriteLine(originalList.PrintForward());
            Console.WriteLine(list1.PrintForward());
        }

