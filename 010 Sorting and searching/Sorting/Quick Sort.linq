<Query Kind="Program" />

public static void Swap(int[] array, int i, int j)
{
    int tmp = array[i];
    array[i] = array[j];
    array[j] = tmp;
}

public static int Partition(int[] arr, int left, int right)
{
    int pivot = arr[(left + right) / 2]; // Pick a pivot point. Can be an element		

    while (left <= right)
    { // Until we've gone through the whole array
      // Find element on left that should be on right
        while (arr[left] < pivot)
        {
            left++;
        }

        // Find element on right that should be on left
        while (arr[right] > pivot)
        {
            right--;
        }

        // Swap elements, and move left and right indices
        if (left <= right)
        {
            Swap(arr, left, right);
            left++;
            right--;
        }
    }
    return left;
}

public static void QuickSort(int[] arr, int left, int right)
{
    int index = Partition(arr, left, right);
    if (left < index - 1)
    { // Sort left half
        QuickSort(arr, left, index - 1);
    }
    if (index < right)
    { // Sort right half
        QuickSort(arr, index, right);
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
    QuickSort(arr, 0, arr.Length-1);
    PrintIntArray(arr);
}
