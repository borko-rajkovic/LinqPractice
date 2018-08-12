<Query Kind="Program" />

class Program
{
    public class HtWt : IComparable<HtWt>
    {
        private int height;
        private int weight;
        public HtWt(int h, int w) { height = h; weight = w; }

        public override String ToString()
        {
            return "(" + height + ", " + weight + ")";
        }

        public Boolean IsBefore(HtWt other)
        {
            if (height < other.height && weight < other.weight)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int CompareTo(HtWt second)
        {
            if (height != second.height)
            {
                return height.CompareTo(second.height);
            }
            else
            {
                return weight.CompareTo(second.weight);
            }
        }
    }

    // Returns longer sequence
    private static List<HtWt> Max(List<HtWt> seq1, List<HtWt> seq2)
    {
        if (seq1 == null)
        {
            return seq2;
        }
        else if (seq2 == null)
        {
            return seq1;
        }
        return seq1.Count > seq2.Count ? seq1 : seq2;
    }

    private static Boolean CanAppend(List<HtWt> solution, HtWt value)
    {
        if (solution == null)
        {
            return false;
        }
        if (solution.Count == 0)
        {
            return true;
        }
        HtWt last = solution[solution.Count - 1];
        return last.IsBefore(value);
    }

    public static List<HtWt> LongestIncreasingSeq(List<HtWt> array)
    {
        array.Sort();

        List<List<HtWt>> solutions = new List<List<HtWt>>();
        List<HtWt> bestSequence = null;
        for (int i = 0; i < array.Count; i++)
        {
            List<HtWt> longestAtIndex = BestSeqAtIndex(array, solutions, i);
            solutions.Add(longestAtIndex);
            bestSequence = Max(bestSequence, longestAtIndex);
        }

        return bestSequence;
    }

    private static List<HtWt> BestSeqAtIndex(List<HtWt> array, List<List<HtWt>> solutions, int index)
    {
        HtWt value = array[index];

        List<HtWt> bestSequence = new List<HtWt>();

        for (int i = 0; i < index; i++)
        {
            List<HtWt> solution = solutions[i];
            if (CanAppend(solution, value))
            {
                bestSequence = Max(solution, bestSequence);
            }
        }

        List<HtWt> best = new List<HtWt>();
        foreach (var item in bestSequence)
        {
            best.Add(item);
        }


        best.Add(value);

        return best;
    }

    public static List<HtWt> Initialize()
    {
        List<HtWt> items = new List<HtWt>();

        HtWt item = new HtWt(65, 60);
        items.Add(item);

        item = new HtWt(70, 150);
        items.Add(item);

        item = new HtWt(56, 90);
        items.Add(item);

        item = new HtWt(75, 190);
        items.Add(item);

        item = new HtWt(60, 95);
        items.Add(item);

        item = new HtWt(68, 110);
        items.Add(item);

        item = new HtWt(35, 65);
        items.Add(item);

        item = new HtWt(40, 60);
        items.Add(item);

        item = new HtWt(45, 63);
        items.Add(item);

        return items;
    }

    public static void PrintList(List<HtWt> list)
    {
        foreach (HtWt item in list)
        {
            Console.WriteLine(item.ToString() + " ");
        }
    }

    public static void Main(String[] args)
    {
        List<HtWt> items = Initialize();
        List<HtWt> solution = LongestIncreasingSeq(items);
        PrintList(solution);
    }
}
