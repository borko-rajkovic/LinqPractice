<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> list;
        public int Count { get { return list.Count; } }
        public readonly bool IsDescending;

        public PriorityQueue()
        {
            list = new List<T>();
        }

        public PriorityQueue(bool isdesc)
            : this()
        {
            IsDescending = isdesc;
        }

        public PriorityQueue(int capacity)
            : this(capacity, false)
        { }

        public PriorityQueue(IEnumerable<T> collection)
            : this(collection, false)
        { }

        public PriorityQueue(int capacity, bool isdesc)
        {
            list = new List<T>(capacity);
            IsDescending = isdesc;
        }

        public PriorityQueue(IEnumerable<T> collection, bool isdesc)
            : this()
        {
            IsDescending = isdesc;
            foreach (var item in collection)
                Enqueue(item);
        }


        public void Enqueue(T x)
        {
            list.Add(x);
            int i = Count - 1;

            while (i > 0)
            {
                int p = (i - 1) / 2;
                if ((IsDescending ? -1 : 1) * list[p].CompareTo(x) <= 0) break;

                list[i] = list[p];
                i = p;
            }

            if (Count > 0) list[i] = x;
        }

        public T Dequeue()
        {
            T target = Peek();
            T root = list[Count - 1];
            list.RemoveAt(Count - 1);

            int i = 0;
            while (i * 2 + 1 < Count)
            {
                int a = i * 2 + 1;
                int b = i * 2 + 2;
                int c = b < Count && (IsDescending ? -1 : 1) * list[b].CompareTo(list[a]) < 0 ? b : a;

                if ((IsDescending ? -1 : 1) * list[c].CompareTo(root) >= 0) break;
                list[i] = list[c];
                i = c;
            }

            if (Count > 0) list[i] = root;
            return target;
        }

        public T Peek()
        {
            if (Count == 0) throw new InvalidOperationException("Queue is empty.");
            return list[0];
        }

        public void Clear()
        {
            list.Clear();
        }
    }

    public class MinHeapComparator : IComparer<int>
    {
        // Comparator that sorts integers from lowest to highest

        public int Compare(int o1, int o2)
        {
            if (o1 > o2) return 1;
            else if (o1 == o2) return 0;
            else return -1;
        }

    }

    public class MaxHeapComparator : IComparer<int>
    {
        // Comparator that sorts integers from highest to lowest
        public int Compare(int o1, int o2)
        {
            // TODO Auto-generated method stub
            if (o1 < o2) return 1;
            else if (o1 == o2) return 0;
            else return -1;
        }
    }


    private static PriorityQueue<int> maxHeap;
    private static PriorityQueue<int> minHeap;

    public static void AddNewNumber(int randomNumber)
    {
        /* Note: addNewNumber maintains a condition that maxHeap.Count >= minHeap.Count */
        if (maxHeap.Count == minHeap.Count)
        {
            if ((minHeap.Count > 0) && randomNumber > minHeap.Peek())
            {
                maxHeap.Enqueue(minHeap.Dequeue());
                minHeap.Enqueue(randomNumber);
            }
            else
            {
                maxHeap.Enqueue(randomNumber);
            }
        }
        else
        {
            if (randomNumber < maxHeap.Peek())
            {
                minHeap.Enqueue(maxHeap.Dequeue());
                maxHeap.Enqueue(randomNumber);
            }
            else
            {
                minHeap.Enqueue(randomNumber);
            }
        }
    }

    public static double GetMedian()
    {
        /* maxHeap is always at least as big as minHeap. So if maxHeap is empty, then minHeap is also. */
        if (maxHeap.Count == 0)
        {
            return 0;
        }
        if (maxHeap.Count == minHeap.Count)
        {
            return ((double)minHeap.Peek() + (double)maxHeap.Peek()) / 2;
        }
        else
        {
            /* If maxHeap and minHeap are of different sizes, then maxHeap must have one extra element. Return maxHeap�s top element.*/
            return maxHeap.Peek();
        }
    }

    public static void AddNewNumberAndPrintMedian(int randomNumber)
    {
        AddNewNumber(randomNumber);
        Console.WriteLine("Random Number = " + randomNumber);
        PrintMinHeapAndMaxHeap();
        Console.WriteLine("\nMedian = " + GetMedian() + "\n");
    }

    public static void PrintMinHeapAndMaxHeap()
    {
        int[] minHeapArray = new int[minHeap.Count];

        for (int i = 0; i < minHeap.Count; i++)
        {
            minHeapArray[i] = minHeap.Dequeue();
        }

        int[] maxHeapArray = new int[maxHeap.Count];

        for (int i = 0; i < maxHeap.Count; i++)
        {
            maxHeapArray[i] = maxHeap.Dequeue();
        }

        Array.Sort(minHeapArray);
        Array.Sort(maxHeapArray);
        Array.Reverse(maxHeapArray);

        Console.Write("MinHeap =");
        for (int i = minHeapArray.Length - 1; i >= 0; i--)
        {
            Console.Write(" " + minHeapArray[i]);
        }
        Console.Write("\nMaxHeap =");
        for (int i = 0; i < maxHeapArray.Length; i++)
        {
            Console.Write(" " + maxHeapArray[i]);
        }
    }

    public static Random random = new Random();

    public static void Main(String[] args)
    {
        int arraySize = 10;
        int range = 7;

        maxHeap = new PriorityQueue<int>(arraySize - arraySize / 2, true);
        minHeap = new PriorityQueue<int>(arraySize / 2, false);

        for (int i = 0; i < arraySize; i++)
        {
            int randomNumber = random.Next(range + 1);
            AddNewNumberAndPrintMedian(randomNumber);
        }

    }
}
