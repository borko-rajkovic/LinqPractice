<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int CountOnes(int i)
    {
        int count = 0;
        while (i > 0)
        {
            if ((i & 1) == 1)
            {
                count++;
            }
            i = i >> 1;
        }
        return count;
    }

    public static int CountZeros(int i)
    {
        return 32 - CountOnes(i);
    }

    public static Boolean HasValidNext(int i)
    {
        if (i == 0)
        {
            return false;
        }
        int count = 0;
        while ((i & 1) == 0)
        {
            i >>= 1;
            count++;
        }
        while ((i & 1) == 1)
        {
            i >>= 1;
            count++;
        }
        if (count == 31)
        {
            return false;
        }
        return true;
    }

    public static Boolean HasValidPrev(int i)
    {
        while ((i & 1) == 1)
        {
            i >>= 1;
        }
        if (i == 0)
        {
            return false;
        }
        return true;
    }

    public static int GetNextSlow(int i)
    {
        if (!HasValidNext(i))
        {
            return -1;
        }
        int num_ones = CountOnes(i);
        i++;
        while (CountOnes(i) != num_ones)
        {
            i++;
        }
        return i;
    }

    public static int GetPrevSlow(int i)
    {
        if (!HasValidPrev(i))
        {
            return -1;
        }
        int num_ones = CountOnes(i);
        i--;
        while (CountOnes(i) != num_ones)
        {
            i--;
        }
        return i;
    }

    public static void BinPrint(int n)
    {
        Console.WriteLine(Convert.ToString(n, 2));
    }

    public static void Main(String[] args)
    {
        int i = 227;
        int p1 = GetPrevSlow(i);
        int n1 = GetNextSlow(i);
        BinPrint(i);
        Console.WriteLine();
        BinPrint(p1);
        BinPrint(n1);
    }
}
