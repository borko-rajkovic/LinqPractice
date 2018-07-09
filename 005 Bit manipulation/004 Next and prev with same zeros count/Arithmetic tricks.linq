<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int GetNext(int n)
    {
        int c = n;
        int c0 = 0;
        int c1 = 0;
        while (((c & 1) == 0) && (c != 0))
        {
            c0++;
            c >>= 1;
        }

        while ((c & 1) == 1)
        {
            c1++;
            c >>= 1;
        }

        /* If c is 0, then n is a sequence of 1s followed by a sequence of 0s. This is already the biggest
         * number with c1 ones. Return error.
         */
        if (c0 + c1 == 31 || c0 + c1 == 0)
        {
            return -1;
        }

        /* Arithmetically:
         * 2^c0 = 1 << c0
         * 2^(c1-1) = 1 << (c0 - 1)
         * next = n + 2^c0 + 2^(c1-1) - 1;
         */

        return n + (1 << c0) + (1 << (c1 - 1)) - 1;
    }

    public static int GetPrev(int n)
    {
        int temp = n;
        int c0 = 0;
        int c1 = 0;
        while (((temp & 1) == 1) && (temp != 0))
        {
            c1++;
            temp >>= 1;
        }

        /* If temp is 0, then the number is a sequence of 0s followed by a sequence of 1s. This is already
         * the smallest number with c1 ones. Return -1 for an error.
         */
        if (temp == 0)
        {
            return -1;
        }

        while ((temp & 1) == 0 && (temp != 0))
        {
            c0++;
            temp >>= 1;
        }

        /* Arithmetic:
         * 2^c1 = 1 << c1
         * 2^(c0 - 1) = 1 << (c0 - 1)
         */
        return n - (1 << c1) - (1 << (c0 - 1)) + 1;
    }

    public static void BinPrint(int n)
    {
        Console.WriteLine(Convert.ToString(n, 2));
    }

    public static void Main(String[] args)
    {
        int i = 227;
        int p1 = GetPrev(i);
        int n1 = GetNext(i);
        BinPrint(i);
        Console.WriteLine();
        BinPrint(p1);
        BinPrint(n1);
    }
}
