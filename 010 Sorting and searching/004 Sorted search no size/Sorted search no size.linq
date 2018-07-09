<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Listy
    {
        int[] array;

        public Listy(int[] arr)
        {
            array = (int[])arr.Clone();
        }

        public int ElementAt(int index)
        {
            if (index >= array.Length)
            {
                return -1;
            }
            return array[index];
        }
    }

    public static int BinarySearch(Listy list, int value, int low, int high)
    {
        int mid;

        while (low <= high)
        {
            mid = (low + high) / 2;
            int middle = list.ElementAt(mid);
            if (middle > value || middle == -1)
            {
                high = mid - 1;
            }
            else if (middle < value)
            {
                low = mid + 1;
            }
            else
            {
                return mid;
            }
        }
        return -1;
    }

    public static int Search(Listy list, int value)
    {
        int index = 1;
        while (list.ElementAt(index) != -1 && list.ElementAt(index) < value)
        {
            index *= 2;
        }
        return BinarySearch(list, value, index / 2, index);
    }

    public static void Main(String[] args)
    {
        int[] array = { 1, 2, 4, 5, 6, 7, 9, 10, 11, 12, 13, 14, 16, 18 };
        Listy list = new Listy(array);
        foreach (int a in array)
        {
            Console.WriteLine(Search(list, a));
        }
        Console.WriteLine(Search(list, 15));
    }
}
