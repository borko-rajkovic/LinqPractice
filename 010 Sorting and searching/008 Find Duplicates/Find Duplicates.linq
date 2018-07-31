<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    private static readonly Random RandomIntNumbers = new Random();

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int[] RandomArray(int N, int min, int max)
    {
        int[] array = new int[N];
        for (int j = 0; j < N; j++)
        {
            array[j] = RandomIntInRange(min, max);
        }
        return array;
    }

    public static string ArrayToString(int[] array)
    {
        StringBuilder sb = new StringBuilder();
        foreach (int v in array)
        {
            sb.AppendFormat("{0}, ", v);
        }
        return sb.ToString();
    }

    class BitSet
    {
        int[] bitset;

        public BitSet(int size)
        {
            bitset = new int[(size >> 5) + 1]; // divide by 32
        }

        public Boolean Get(int pos)
        {
            int wordNumber = (pos >> 5); // divide by 32
            int bitNumber = (pos & 0x1F); // mod 32
            return (bitset[wordNumber] & (1 << bitNumber)) != 0;
        }

        public void Set(int pos)
        {
            int wordNumber = (pos >> 5); // divide by 32
            int bitNumber = (pos & 0x1F); // mod 32
            bitset[wordNumber] |= 1 << bitNumber;
        }
    }

    public static void CheckDuplicates(int[] array)
    {
        BitSet bs = new BitSet(32000);
        for (int i = 0; i < array.Length; i++)
        {
            int num = array[i];
            int num0 = num - 1; // bitset starts at 0, numbers start at 1
            if (bs.Get(num0))
            {
                Console.WriteLine(num);
            }
            else
            {
                bs.Set(num0);
            }
        }
    }

    public static void Main(String[] args)
    {
        int[] array = RandomArray(30, 1, 30);
        Console.WriteLine(ArrayToString(array));
        CheckDuplicates(array);
    }
}
