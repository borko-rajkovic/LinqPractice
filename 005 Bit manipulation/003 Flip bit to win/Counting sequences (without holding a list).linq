<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    /* Given set of three sequences ordered as {0s, then 1s, then 0s}, 
     * find max sequence that can be formed. */
    public static int GetMaxSequence(int[] sequences)
    { /* 1s, then 0s, then [old] ones */
        if (sequences[1] == 1)
        { // a single 0 -> merge sequences
            return sequences[0] + sequences[2] + 1;
        }
        else if (sequences[1] == 0)
        { // no 0s -> take one side
            return Math.Max(sequences[0], sequences[2]);
        }
        else
        {  // many 0s -> take side, add 1 (flip a bit)
            return Math.Max(sequences[0], sequences[2]) + 1;
        }
    }

    public static void Shift(int[] sequences)
    {
        sequences[2] = sequences[1];
        sequences[1] = sequences[0];
        sequences[0] = 0;
    }

    public static int LongestSequence(uint n)
    {
        uint searchingFor = 0;
        int[] sequences = { 0, 0, 0 }; // Counts of last 3 sequences
        int maxSequence = 1;

        for (int i = 0; i < sizeof(int)*8; i++)
        {
            if ((n & 1) != searchingFor)
            {
                if (searchingFor == 1)
                { // End of 1s + 0s + 1s sequence
                    maxSequence = Math.Max(maxSequence, GetMaxSequence(sequences));
                }

                searchingFor = n & 1; // Flip 1 to 0 or 0 to 1
                Shift(sequences); // Shift sequences
            }
            sequences[0]++;
            n >>= 1;
        }

        /* Check final set of sequences */
        if (searchingFor == 0)
        {
            Shift(sequences);
        }
        int finalSequence = GetMaxSequence(sequences);
        maxSequence = Math.Max(finalSequence, maxSequence);

        return maxSequence;
    }

    public static void Main(String[] args)
    {
        int original_number = 1775;
        int new_number = LongestSequence((uint) original_number);

        Console.WriteLine(Convert.ToString(original_number, 2));
        Console.WriteLine(new_number);
    }
}
