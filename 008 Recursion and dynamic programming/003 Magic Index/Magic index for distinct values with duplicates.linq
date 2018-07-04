<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MagicSlow(int[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == i)
            {
                return i;
            }
        }
        return -1;
    }

    public static int MagicFast(int[] array, int start, int end)
    {
        if (end < start)
        {
            return -1;
        }
        int midIndex = (start + end) / 2;
        int midValue = array[midIndex];
        if (midValue == midIndex)
        {
            return midIndex;
        }
        /* Search left */
        int leftIndex = Math.Min(midIndex - 1, midValue);
        int left = MagicFast(array, start, leftIndex);
        if (left >= 0)
        {
            return left;
        }

        /* Search right */
        int rightIndex = Math.Max(midIndex + 1, midValue);
        int right = MagicFast(array, rightIndex, end);

        return right;
    }

    public static int MagicFast(int[] array)
    {
        return MagicFast(array, 0, array.Length - 1);
    }

    public static Random random = new Random();

    public static int RandomInt(int n)
    {
        return random.Next(n);
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

    /* Creates an array that is distinct and sorted */
    public static int[] GetDistinctSortedArray(int size)
    {
        int[] array = RandomArray(size, -1 * size, size);
        Array.Sort(array);
        for (int i = 1; i < array.Length; i++)
        {
            if (array[i] == array[i - 1])
            {
                array[i]++;
            }
            else if (array[i] < array[i - 1])
            {
                array[i] = array[i - 1] + 1;
            }
        }
        return array;
    }

    public static int[] CreateArrayWithDuplicates(int size)
    {
        int[] array = new int[size];

        int k = size/2;

        for (int i = 0; i < size; i++)
        {
            array[i] = k;
            if (i % 100 == 0) k++;
        }

        return array;
    }

    public static void Main(String[] args)
    {
        Console.WriteLine("Test 1");
        Console.WriteLine();

        for (int i = 10000000; i < 10000005; i++)
        {
            // This will be slower than naive
            int[] array = Enumerable.Range(3, i).ToArray<int>();

            var watch = System.Diagnostics.Stopwatch.StartNew();
            int v1 = MagicSlow(array);
            watch.Stop();

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            int v2 = MagicFast(array);
            watch2.Stop();

            Console.WriteLine($"v1={watch.ElapsedMilliseconds}ms; v2={watch2.ElapsedMilliseconds}ms");
            Console.WriteLine($"v1={watch.ElapsedTicks} ticks; v2={watch2.ElapsedTicks} ticks");
        }
        Console.WriteLine();

        Console.WriteLine("Test 2");
        Console.WriteLine();

        for (int i = 10000000; i < 10000005; i++)
        {
            // This will be faster
            int[] array = CreateArrayWithDuplicates(i);

            var watch = System.Diagnostics.Stopwatch.StartNew();
            int v1 = MagicSlow(array);
            watch.Stop();

            var watch2 = System.Diagnostics.Stopwatch.StartNew();
            int v2 = MagicFast(array);
            watch2.Stop();

            Console.WriteLine($"v1={watch.ElapsedMilliseconds}ms; v2={watch2.ElapsedMilliseconds}ms");
            Console.WriteLine($"v1={watch.ElapsedTicks} ticks; v2={watch2.ElapsedTicks} ticks");
        }
    }
}
