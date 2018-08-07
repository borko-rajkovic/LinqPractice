<Query Kind="Program" />

class Program
{
    public static void SortValleyPeak(int[] array)
    {
        for (int i = 1; i < array.Length; i += 2)
        {
            int biggestIndex = MaxIndex(array, i - 1, i, i + 1);
            if (i != biggestIndex)
            {
                Swap(array, i, biggestIndex);
            }
        }
    }

    public static int MaxIndex(int[] array, int a, int b, int c)
    {
        int len = array.Length;
        int aValue = a >= 0 && a < len ? array[a] : int.MinValue;
        int bValue = b >= 0 && b < len ? array[b] : int.MinValue;
        int cValue = c >= 0 && c < len ? array[c] : int.MinValue;

        int max = Math.Max(aValue, Math.Max(bValue, cValue));

        if (aValue == max)
        {
            return a;
        }
        else if (bValue == max)
        {
            return b;
        }
        else
        {
            return c;
        }
    }


    public static void Swap(int[] array, int left, int right)
    {
        int temp = array[left];
        array[left] = array[right];
        array[right] = temp;
    }

    public static Boolean ConfirmValleyPeak(int[] array)
    {
        for (int i = 1; i < array.Length - 1; i++)
        {
            int prev = array[i - 1];
            int curr = array[i];
            int next = array[i + 1];
            if (prev <= curr && curr >= next)
            {
                continue;
            }
            else if (prev >= curr && curr <= next)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    public static string ArrayToString(int[] array)
    {
        return string.Join(", ", array);
    }

    public static void Main(String[] args)
    {
        int[] array = { 48, 40, 31, 62, 28, 21, 64, 40, 23, 17 };
        Console.WriteLine(ArrayToString(array));
        SortValleyPeak(array);
        Console.WriteLine(ArrayToString(array));
        Console.WriteLine(ConfirmValleyPeak(array));
    }
}
