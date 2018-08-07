<Query Kind="Program" />

class Program
{
    public static void SortValleyPeak(int[] array)
    {
        for (int i = 1; i < array.Length; i += 2)
        {
            if (array[i - 1] < array[i])
            {
                Swap(array, i - 1, i);
            }
            if (i + 1 < array.Length && array[i + 1] < array[i])
            {
                Swap(array, i + 1, i);
            }
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

        Console.WriteLine();
        int[] array2 = { 5, 3, 1, 2, 3 };
        Console.WriteLine(ArrayToString(array2));
        SortValleyPeak(array2);
        Console.WriteLine(ArrayToString(array2));
        Console.WriteLine(ConfirmValleyPeak(array2));

    }
}
