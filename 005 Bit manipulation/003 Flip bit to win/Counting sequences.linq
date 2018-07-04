<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int LongestSequence(int n)
    {
        if (n == -1) return sizeof(int) * 8;
        List<int> sequences = GetAlternatingSequences((uint)n);
        return FindLongestSequence(sequences);
    }

    /* Return a list of the sizes of the sequences. The sequence starts 
     * off with the number of 0s (which might be 0) and then alternates
     * with the counts of each value.*/
    public static List<int> GetAlternatingSequences(uint n)
    {
        List<int> sequences = new List<int>();

        uint searchingFor = 0;
        int counter = 0;

        for (int i = 0; i < sizeof(int) * 8; i++)
        {
            if ((n & 1) != searchingFor)
            {
                sequences.Add(counter);
                searchingFor = n & 1; // Flip 1 to 0 or 0 to 1
                counter = 0;
            }
            counter++;
            n >>= 1;
        }
        sequences.Add(counter);

        return sequences;
    }

    public static int FindLongestSequence(List<int> seq)
    {
        int maxSeq = 1;

        for (int i = 0; i < seq.Count; i += 2)
        {
            int zerosSeq = seq[i];
            int onesSeqRight = i - 1 >= 0 ? seq[i - 1] : 0;
            int onesSeqLeft = i + 1 < seq.Count ? seq[i + 1] : 0;

            int thisSeq = 0;
            if (zerosSeq == 1)
            { // Can merge
                thisSeq = onesSeqLeft + 1 + onesSeqRight;
            }
            if (zerosSeq > 1)
            { // Just add a zero to either side
                thisSeq = 1 + Math.Max(onesSeqRight, onesSeqLeft);
            }
            else if (zerosSeq == 0)
            { // No zero, but take either side
                thisSeq = Math.Max(onesSeqRight, onesSeqLeft);
            }
            maxSeq = Math.Max(thisSeq, maxSeq);
        }

        return maxSeq;
    }

    public static void Main(String[] args)
    {
        int original_number = 1775;
        int new_number = LongestSequence(original_number);

        Console.WriteLine(Convert.ToString(original_number, 2));
        Console.WriteLine(new_number);
    }
}
