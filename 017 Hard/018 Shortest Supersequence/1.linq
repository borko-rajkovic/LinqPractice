<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
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

    /* Find next instance of element starting from index. */
    public static int FindNextInstance(int[] array, int element, int index)
    {
        for (int i = index; i < array.Length; i++)
        {
            if (array[i] == element)
            {
                return i;
            }
        }
        return -1;
    }

    /* Given an index, find the closure (i.e., the element which terminates a complete
     * subarray containing all elements in smallArray). This will be the max of the
     * next locations of each element in smallArray. */
    public static int FindClosure(int[] bigArray, int[] smallArray, int index)
    {
        int max = -1;
        for (int i = 0; i < smallArray.Length; i++)
        {
            int next = FindNextInstance(bigArray, smallArray[i], index);
            if (next == -1)
            {
                return -1;
            }
            max = Math.Max(next, max);
        }
        return max;
    }

    public static Range ShortestSupersequence(int[] bigArray, int[] smallArray)
    {
        int bestStart = -1;
        int bestEnd = -1;
        for (int i = 0; i < bigArray.Length; i++)
        {
            int end = FindClosure(bigArray, smallArray, i);
            if (end == -1) break;
            if (bestStart == -1 || end - i < bestEnd - bestStart)
            {
                bestStart = i;
                bestEnd = end;
            }
        }
        return new Range(bestStart, bestEnd);
    }

    public static void Main(String[] args)
    {
        int[] array = { 7, 5, 9, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 9, 7 };
        int[] set = { 1, 5, 9 };
        Console.WriteLine(array.Length);
        Range shortest = ShortestSupersequence(array, set);
        Console.WriteLine(shortest.GetStart() + ", " + shortest.GetEnd());
    }
}
