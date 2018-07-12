<Query Kind="Program" />

class Program
{
	public static int totalCount;
    public static int CountFactZeros(int num)
    {
        int count = 0;
        if (num < 0)
        {
            Console.WriteLine("Factorial is not defined for negative numbers");
            return 0;
        }
        for (int i = 5; num / i > 0; i *= 5)
        {
			totalCount++;
            count += num / i;
        }
        return count;
    }

    public static void Main(String[] args)
    {		
        for (int i = (int.MaxValue/100-10); i < (int.MaxValue/100); i++)
        {
			totalCount=0;
            Console.WriteLine(i + " has " + CountFactZeros(i) + " zeros; Total operations = "+totalCount);
        }
    }
}
