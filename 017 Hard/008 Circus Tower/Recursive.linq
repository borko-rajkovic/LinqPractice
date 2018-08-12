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

    public static List<HtWt> LongestIncreasingSeq(List<HtWt> items)
    {
        items.Sort();
        return BestSeqAtIndex(items, new List<HtWt>(), 0);
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

    private static List<HtWt> BestSeqAtIndex(List<HtWt> array, List<HtWt> sequence, int index)
    {
        if (index >= array.Count) return sequence;

        HtWt value = array[index];

        List<HtWt> bestWith = null;
        if (CanAppend(sequence, value))
        {
            List<HtWt> sequenceWith = new List<HtWt>();
            foreach (var item in sequence)
            {
                sequenceWith.Add(item);
            }

            sequenceWith.Add(value);
            bestWith = BestSeqAtIndex(array, sequenceWith, index + 1);
        }

        List<HtWt> bestWithout = BestSeqAtIndex(array, sequence, index + 1);
        return Max(bestWith, bestWithout);
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
