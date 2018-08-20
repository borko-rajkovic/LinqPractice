<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int Rand7()
    {
        while (true)
        {
            int num = 5 * Rand5() + Rand5();
            if (num < 21)
            {
                return num % 7;
            }
        }
    }

    public static Random random = new Random();

    public static int Rand5()
    {
        return random.Next(100) % 5;
    }

    public static void Main(String[] args)
    {
        /* Test: call rand7 many times and inspect the results. */
        int[] arr = new int[7];
        int test_size = 1000000;
        for (int k = 0; k < test_size; k++)
        {
            arr[Rand7()]++;
        }

        for (int i = 0; i < 7; i++)
        {
            double percent = 100.0 * arr[i] / test_size;
            Console.WriteLine(i + " appeared " + percent + "% of the time.");
        }
    }
}
