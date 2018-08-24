<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Numerics.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
  <Namespace>System.Numerics</Namespace>
</Query>

class Program
{

    public static BigInteger ProductToN(int n)
    {
        BigInteger fullProduct = new BigInteger(1);
        for (int i = 2; i <= n; i++)
        {
            fullProduct = fullProduct * (new BigInteger(i));
        }
        return fullProduct;
    }

    public static int MissingOne(int[] array)
    {
        BigInteger fullProduct = ProductToN(array.Length + 1);

        BigInteger actualProduct = new BigInteger(1);
        for (int i = 0; i < array.Length; i++)
        {
            BigInteger value = new BigInteger(array[i]);
            actualProduct = actualProduct * value;
        }

        BigInteger missingNumber = fullProduct / actualProduct;
        return Int32.Parse(missingNumber.ToString());
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
