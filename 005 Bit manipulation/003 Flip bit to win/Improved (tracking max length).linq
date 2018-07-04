<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int FlipBit(uint a)
    {
        /* If all 1s, this is already the longest sequence. */
        if (~a == 0) return sizeof(int) * 8;

        int currentLength = 0;
        int previousLength = 0;
        int maxLength = 1; // We can always have a sequence of at least one 1
        while (a != 0)
        {
            if ((a & 1) == 1)
            {
                currentLength++;
            }
            else if ((a & 1) == 0)
            {
                /* Update to 0 (if next bit is 0) or currentLength (if next bit is 1). */
                previousLength = (a & 2) == 0 ? 0 : currentLength;
                currentLength = 0;
            }
            maxLength = Math.Max(previousLength + currentLength + 1, maxLength);
            a >>= 1;
        }
        return maxLength;
    }

    public static void Main(String[] args)
    {
        int original_number = 1775;
        int new_number = FlipBit((uint)original_number);

        Console.WriteLine(Convert.ToString(original_number, 2));
        Console.WriteLine(new_number);
    }
}
