<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class PriorityQueue<T> where T : IComparable
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

    public static int[] SmallestK(int[] array, int k)
    {
        if (k <= 0 || k > array.Length)
        {
            throw new Exception();
        }

        PriorityQueue<int> heap = GetKMaxHeap(array, k);
        return HeapToIntArray(heap);
    }

    /* Convert heap to int array. */
    public static int[] HeapToIntArray(PriorityQueue<int> heap)
    {
        int[] array = new int[heap.Count];
        while (heap.Count>0)
            {
            array[heap.Count - 1] = heap.Dequeue();
        }
        return array;
    }

    /* Create max heap of smallest k elements. */
    public static PriorityQueue<int> GetKMaxHeap(int[] array, int k)
    {
        PriorityQueue<int> heap = new PriorityQueue<int>(k, true);
        foreach (int a in array)
        {
            if (heap.Count < k)
            { // If space remaining
                heap.Enqueue(a);
            }
            else if (a < heap.Peek())
            { // If full and top is small
                heap.Dequeue(); // remove highest
                heap.Enqueue(a); // insert new element
            }
        }
        return heap;
    }

    public static void Main(String[] args)
    {
        int[] array = { 1, 5, 2, 9, -1, 11, 6, 13, 15 };
        int[] smallest = SmallestK(array, 3);
        Console.WriteLine(string.Join(", ", smallest));
    }
}
