<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int MissingOne(int[] array)
    {
        int max_value = array.Length + 1;
        int remainder = max_value * (max_value + 1) / 2;

        for (int i = 0; i < array.Length; i++)
        {
            remainder -= array[i];
        }
        return remainder;
    }

    public static void Main(String[] args)
    {
        int max = 100;
        int x = 8;
        int len = max - 1;
        int count = 0;
        int[] array = new int[len];
        for (int i = 1; i <= max; i++)
        {
            if (i != x)
            {
                array[count] = i;
                count++;
            }
        }
        Console.WriteLine(x);
        int solution = MissingOne(array);

        Console.WriteLine(solution);

    }
}
