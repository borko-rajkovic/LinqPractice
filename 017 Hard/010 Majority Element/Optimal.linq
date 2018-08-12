<Query Kind="Program" />

class Program
{
    public static int GetCandidate(int[] array)
    {
        int majority = 0;
        int count = 0;
        foreach (int n in array)
        {
            if (count == 0)
            {
                majority = n;
            }
            if (n == majority)
            {
                count++;
            }
            else
            {
                count--;
            }
        }
        return majority;
    }

    public static Boolean Validate(int[] array, int majority)
    {
        int count = 0;
        foreach (int n in array)
        {
            if (n == majority)
            {
                count++;
            }
        }

        return count > array.Length / 2;
    }

    public static int FindMajorityElement(int[] array)
    {
        int candidate = GetCandidate(array);
        return Validate(array, candidate) ? candidate : -1;
    }

    public static void Main(String[] args)
    {
        int[] array = { 0, 0, 1, 2, 2, 0, 1, 0, 1, 1, 1, 1, 1 };
        Console.WriteLine(array.Length);
        Console.WriteLine(FindMajorityElement(array));
    }
}
