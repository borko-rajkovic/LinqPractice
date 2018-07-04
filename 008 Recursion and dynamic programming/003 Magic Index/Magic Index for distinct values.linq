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
        int mid = (start + end) / 2;
        if (array[mid] == mid)
        {
            return mid;
        }
        else if (array[mid] > mid)
        {
            return MagicFast(array, start, mid - 1);
        }
        else
        {
            return MagicFast(array, mid + 1, end);
        }
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

    public static void Main(String[] args)
    {
        for (int i = 10000000; i < 10000011; i++)
        {
            //int[] array = GetDistinctSortedArray(i);
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
    }
}
