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

        private static int _tapB = 0;
        private static int _tapC = 0;

        private static void Tap(int i)
        {
            if (i == 0)
            {
                _tapB++;
            }
            else
            {
                _tapC++;
            }
        }

        private static void DeleteDupsB(LinkedListNode head)
        {
            if (head == null) return;

            var current = head;

            while (current != null)
            {
                /* Remove all future nodes that have the same value */
                var runner = current;

                while (runner.Next != null)
                {
                    Tap(0);

                    if (runner.Next.Data == current.Data)
                    {
                        runner.Next = runner.Next.Next;
                    }
                    else
                    {
                        runner = runner.Next;
                    }
                }
                current = current.Next;
            }
        }

        private static void DeleteDupsC(LinkedListNode head)
        {
            if (head == null) return;

            var previous = head;
            var current = previous.Next;

            while (current != null)
            {
                // Look backwards for dups, and remove any that you see.
                var runner = head;

                while (runner != current)
                {
                    Tap(1);

                    if (runner.Data == current.Data)
                    {
                        var temp = current.Next;
                        previous.Next = temp;
                        current = temp;
                        /* We know we can't have more than one dup preceding
                         * our element since it would have been removed
                         * earlier. */
                        break;
                    }
                    runner = runner.Next;
                }

                /* If runner == current, then we didn�t find any duplicate
                 * elements in the previous for loop.  We then need to
                 * increment current.
                 * If runner != current, then we must have hit the �break�
                 * condition, in which case we found a dup and current has
                 * already been incremented.*/
                if (runner == current)
                {
                    previous = current;
                    current = current.Next;
                }
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

            var list2 = originalList.Clone();
            var list3 = originalList.Clone();

			_tapB = 0;
			_tapC = 0;

            DeleteDupsB(list2);
            DeleteDupsC(list3);

            Console.WriteLine(originalList.PrintForward());
            Console.WriteLine(list2.PrintForward());
            Console.WriteLine(list3.PrintForward());

            Console.WriteLine(_tapB);
            Console.WriteLine(_tapC);
        }

