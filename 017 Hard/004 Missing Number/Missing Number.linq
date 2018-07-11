<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class BitInteger
    {
        public static int INTEGER_SIZE;
        private readonly Boolean[] bits;
        public BitInteger()
        {
            bits = new Boolean[INTEGER_SIZE];
        }
        /* Creates a number equal to given value. Takes time proportional 
         * to INTEGER_SIZE. */
        public BitInteger(int value)
        {
            bits = new Boolean[INTEGER_SIZE];
            for (int j = 0; j < INTEGER_SIZE; j++)
            {
                if (((value >> j) & 1) == 1) bits[INTEGER_SIZE - 1 - j] = true;
                else bits[INTEGER_SIZE - 1 - j] = false;
            }
        }

        /** Returns k-th most-significant bit. */
        public int Fetch(int k)
        {
            if (bits[k]) return 1;
            else return 0;
        }

        /** Sets k-th most-significant bit. */
        public void Set(int k, int bitValue)
        {
            if (bitValue == 0) bits[k] = false;
            else bits[k] = true;
        }

        /** Sets k-th most-significant bit. */
        public void Set(int k, char bitValue)
        {
            if (bitValue == '0') bits[k] = false;
            else bits[k] = true;
        }

        /** Sets k-th most-significant bit. */
        public void Set(int k, Boolean bitValue)
        {
            bits[k] = bitValue;
        }

        public void SwapValues(BitInteger number)
        {
            for (int i = 0; i < INTEGER_SIZE; i++)
            {
                int temp = number.Fetch(i);
                number.Set(i, Fetch(i));
                Set(i, temp);
            }
        }

        public int ToInt()
        {
            int number = 0;
            for (int j = INTEGER_SIZE - 1; j >= 0; j--)
            {
                number = number | Fetch(j);
                if (j > 0)
                {
                    number = number << 1;
                }
            }
            return number;
        }
    }

    public static Random random = new Random();

    /* Create a random array of numbers from 0 to n, but skip 'missing' */
    public static List<BitInteger> Initialize(int n, int missing)
    {
        BitInteger.INTEGER_SIZE = Convert.ToString(n, 2).Length;
        List<BitInteger> array = new List<BitInteger>();

        for (int i = 1; i <= n; i++)
        {
            array.Add(new BitInteger(i == missing ? 0 : i));
        }

        // Shuffle the array once.
        for (int i = 0; i < n; i++)
        {
            int rand = i + random.Next(n-i);
            array[i].SwapValues(array[rand]);
        }

        return array;
    }


    public static int FindMissing(List<BitInteger> array)
    {
        return FindMissing(array, BitInteger.INTEGER_SIZE - 1);
    }

    private static int FindMissing(List<BitInteger> input, int column)
    {
        if (column < 0)
        { // Base case and error condition
            return 0;
        }
        List<BitInteger> oneBits = new List<BitInteger>(input.Count / 2);
        List<BitInteger> zeroBits = new List<BitInteger>(input.Count / 2);
        foreach (BitInteger t in input)
        {
            if (t.Fetch(column) == 0)
            {
                zeroBits.Add(t);
            }
            else
            {
                oneBits.Add(t);
            }
        }
        if (zeroBits.Count <= oneBits.Count)
        {
            int v = FindMissing(zeroBits, column - 1);
            Console.Write("0");
            return (v << 1) | 0;
        }
        else
        {
            int v = FindMissing(oneBits, column - 1);
            Console.Write("1");
            return (v << 1) | 1;
        }
    }

    public static void Main(String[] args)
    {
        Random rand = new Random();
        int n = rand.Next(300000) + 1;
        int missing = rand.Next(n + 1);
        List<BitInteger> array = Initialize(n, missing);
        Console.WriteLine("The array contains all numbers but one from 0 to " + n + ", except for " + missing);

        int result = FindMissing(array);
        Console.WriteLine();
        if (result != missing)
        {
            Console.WriteLine("Error in the algorithm!\n" + "The missing number is " + missing + ", but the algorithm reported " + result);
        }
        else
        {
            Console.WriteLine("The missing number is " + result);
        }
    }
}
