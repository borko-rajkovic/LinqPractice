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

    /* Do backwards sweep to get a list of the next occurrence of value from each index. */
    public static int[] GetNextElement(int[] bigArray, int value)
    {
        int next = -1;
        int[] nexts = new int[bigArray.Length];
        for (int i = bigArray.Length - 1; i >= 0; i--)
        {
            if (bigArray[i] == value)
            {
                next = i;
            }
            nexts[i] = next;
        }
        return nexts;
    }

    /* Create table of next occurrences. */
    public static int[][] GetNextElementsMulti(int[] big, int[] small)
    {
        int[][] nextElements = new int[small.Length][];
        for (int i = 0; i < small.Length; i++)
        {
            nextElements[i] = new int[big.Length];
        }
        for (int i = 0; i < small.Length; i++)
        {
            nextElements[i] = GetNextElement(big, small[i]);
        }
        return nextElements;
    }

    /* Given an index and the table of next elements, find the closure
     * for this index (which will be the min of this column. */
    public static int GetClosureForIndex(int[][] nextElements, int index)
    {
        int max = -1;
        for (int i = 0; i < nextElements.Length; i++)
        {
            if (nextElements[i][index] == -1)
            {
                return -1;
            }
            max = Math.Max(max, nextElements[i][index]);
        }
        return max;
    }

    /* Get closure for each index. */
    public static int[] GetClosures(int[][] nextElements)
    {
        int[] maxNextElement = new int[nextElements[0].Length];
        for (int i = 0; i < nextElements[0].Length; i++)
        {
            maxNextElement[i] = GetClosureForIndex(nextElements, i);
        }
        return maxNextElement;
    }

    /* Get shortest closure. */
    public static Range GetShortestClosure(int[] closures)
    {
        int bestStart = -1;
        int bestEnd = -1;
        for (int i = 0; i < closures.Length; i++)
        {
            if (closures[i] == -1)
            {
                break;
            }
            int current = closures[i] - i;
            if (bestStart == -1 || current < bestEnd - bestStart)
            {
                bestStart = i;
                bestEnd = closures[i];
            }
        }
        return new Range(bestStart, bestEnd);
    }

    public static Range ShortestSupersequence(int[] big, int[] small)
    {
        int[][] nextElements = GetNextElementsMulti(big, small);
        int[] closures = GetClosures(nextElements);
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
