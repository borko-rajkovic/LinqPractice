<Query Kind="Program" />

public static void Mergesort(int[] array)
{
    int[] helper = new int[array.Length];
    Mergesort(array, helper, 0, array.Length - 1);
}

public static void Mergesort(int[] array, int[] helper, int low, int high)
{
    if (low < high)
    {
        int middle = (low + high) / 2;
        Mergesort(array, helper, low, middle); // Sort left half
        Mergesort(array, helper, middle + 1, high); // Sort right half
        Merge(array, helper, low, middle, high); // Merge them
    }
}

public static void Merge(int[] array, int[] helper, int low, int middle, int high)
{
    /* Copy both halves into a helper array */
    for (int i = low; i <= high; i++)
    {
        helper[i] = array[i];
    }

    int helperLeft = low;
    int helperRight = middle + 1;
    int current = low;

    /* Iterate through helper array. Compare the left and right
     * half, copying back the smaller element from the two halves
     * into the original array. */
    while (helperLeft <= middle && helperRight <= high)
    {
        if (helper[helperLeft] <= helper[helperRight])
        {
            array[current] = helper[helperLeft];
            helperLeft++;
        }
        else
        { // If right element is smaller than left element
            array[current] = helper[helperRight];
            helperRight++;
        }
        current++;
    }

    /* Copy the rest of the left side of the array into the
     * target array */
    int remaining = middle - helperLeft;
    for (int i = 0; i <= remaining; i++)
    {
        array[current + i] = helper[helperLeft + i];
    }
}

public static void PrintIntArray(int[] array)
{
    for (int i = 0; i < array.Length; i++)
    {
        Console.Write(array[i] + " ");
    }
    Console.WriteLine();
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

public static int RandomIntInRange(int min, int max)
{
    return RandomInt(max + 1 - min) + min;
}

public static Random r = new Random();

public static int RandomInt(int n)
{
    return (int)(r.Next(n));
}


public static void Main(String[] args)
{
    int[] arr = RandomArray(20, 0, 11);
    PrintIntArray(arr);
    Mergesort(arr);
    PrintIntArray(arr);
}
