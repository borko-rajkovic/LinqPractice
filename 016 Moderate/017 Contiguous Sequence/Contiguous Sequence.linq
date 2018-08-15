<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int GetMaxSum(int[] a)
    {
        int maxSum = 0;
        int runningSum = 0;
        for (int i = 0; i < a.Length; i++)
        {
            runningSum += a[i];
            if (maxSum < runningSum)
            {
                maxSum = runningSum;
            }
            else if (runningSum < 0)
            {
                runningSum = 0;
            }
        }
        return maxSum;
    }

    public static void Main(String[] args)
    {
        int[] a = { 2, -8, 3, -2, 4, -10 };
        Console.WriteLine(GetMaxSum(a));
    }
}
