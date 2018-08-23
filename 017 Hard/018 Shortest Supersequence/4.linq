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

    public class HeapNode : IComparable<HeapNode>
    {

        public int locationWithinList;
        public int listId;

        public HeapNode(int location, int list)
        {
            locationWithinList = location;
            listId = list;
        }

        public int CompareTo(HeapNode other)
        {
            return locationWithinList - other.locationWithinList;
        }
    }

    public class Range
    {
        private int start;
        private int end;
        public Range(int s, int e)
        {
            start = s;
            end = e;
        }

        public int Length()
        {
            return end - start + 1;
        }

        public Boolean ShorterThan(Range other)
        {
            return Length() < other.Length();
        }

        public int GetStart()
        {
            return start;
        }

        public int GetEnd()
        {
            return end;
        }
    }

    public static Range GetShortestClosure(List<Queue<int>> lists)
    {
        PriorityQueue<HeapNode> minHeap = new PriorityQueue<HeapNode>();
        int max = int.MinValue;

        /* Insert min element from each list. */
        for (int i = 0; i < lists.Count; i++)
        {
            int head = lists[i].Dequeue();
            minHeap.Enqueue(new HeapNode(head, i));
            max = Math.Max(max, head);
        }

        int min = minHeap.Peek().locationWithinList;
        int bestRangeMin = min;
        int bestRangeMax = max;

        while (true)
        {
            /* Remove min node. */
            HeapNode n = minHeap.Dequeue();
            Queue<int> list = lists[n.listId];

            /* Compare range to best range. */
            min = n.locationWithinList;
            if (max - min < bestRangeMax - bestRangeMin)
            {
                bestRangeMax = max;
                bestRangeMin = min;
            }

            /* If there are no more elements, then there's no more subsequences and we can break. */
            if (list.Count == 0)
            {
                break;
            }

            /* Add new head of list to heap. */
            n.locationWithinList = list.Dequeue();
            minHeap.Enqueue(n);
            max = Math.Max(max, n.locationWithinList);
        }

        return new Range(bestRangeMin, bestRangeMax);
    }

    /* Get list of queues (linked lists) storing the indices at which
     * each element in smallArray appears in bigArray. */
    public static List<Queue<int>> GetLocationsForElements(int[] big, int[] small)
    {
        /* Initialize hash map from item value to locations. */
        Dictionary<int, Queue<int>> itemLocations = new Dictionary<int, Queue<int>>();
        foreach (int s in small)
        {
            Queue<int> queue = new Queue<int>();
            itemLocations.Add(s, queue);
        }

        /* Walk through big array, adding the item locations to hash map */
        for (int i = 0; i < big.Length; i++)
        {
            Queue<int> queue = itemLocations.ContainsKey(big[i]) ? itemLocations[big[i]] : null;
            if (queue != null)
            {
                queue.Enqueue(i);
            }
        }

        List<Queue<int>> allLocations = new List<Queue<int>>();
        allLocations.AddRange(itemLocations.Values);
        return allLocations;
    }

    public static Range ShortestSupersequence(int[] big, int[] small)
    {
        List<Queue<int>> locations = GetLocationsForElements(big, small);
        if (locations == null) return null;
        return GetShortestClosure(locations);
    }

    public static void Main(String[] args)
    {
        //int[] array = { 7, 5, 9, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 8, 9, 7 };
		int[] array = { 7, 5, 9, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 9, 7 };
        int[] set = { 1, 5, 9 };
        Console.WriteLine(array.Length);
        Range shortest = ShortestSupersequence(array, set);
        if (shortest == null)
        {
            Console.WriteLine("not found");
        }
        else
        {
            Console.WriteLine(shortest.GetStart() + ", " + shortest.GetEnd());
        }
    }
}
