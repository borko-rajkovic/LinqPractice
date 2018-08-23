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

    /* Do backwards sweep and update the closures list with the next occurrence of value, if it's later than the current closure*/
    public static void SweepForClosure(int[] big, int[] closures, int value)
    {
        int next = -1;
        for (int i = big.Length - 1; i >= 0; i--)
        {
            if (big[i] == value)
            {
                next = i;
            }
            if ((next == -1 || closures[i] < next) && (closures[i] != -1))
            {
                closures[i] = next;
            }
        }
    }

    /* Get closure for each index. */
    public static int[] GetClosures(int[] big, int[] small)
    {
        int[] closure = new int[big.Length];
        for (int i = 0; i < small.Length; i++)
        {
            SweepForClosure(big, closure, small[i]);
        }
        return closure;
    }

    /* Get shortest closure. */
    public static Range GetShortestClosure(int[] closures)
    {
        Range shortest = new Range(0, closures[0]);
        for (int i = 1; i < closures.Length; i++)
        {
            if (closures[i] == -1)
            {
                break;
            }
            Range range = new Range(i, closures[i]);
            if (!shortest.ShorterThan(range))
            {
                shortest = range;
            }
        }
        return shortest;
    }

    public static Range ShortestSupersequence(int[] big, int[] small)
    {
        int[] closures = GetClosures(big, small);
        return GetShortestClosure(closures);
    }

    public static void Main(String[] args)
    {
        //int[] array = { 9, 5, 1, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 9, 7 };
        int[] array = { 7, 5, 9, 0, 2, 1, 3, 5, 7, 9, 1, 1, 5, 8, 9, 7 };
        int[] set = { 1, 5, 9 };
        Console.WriteLine(array.Length);
        Range shortest = ShortestSupersequence(array, set);
        Console.WriteLine(shortest.GetStart() + ", " + shortest.GetEnd());
    }
}
