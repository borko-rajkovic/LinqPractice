<Query Kind="Program" />

class Program
{
	public static int totalCount;
	
    public static int FactorsOf5(int i)
    {
        int count = 0;
        while (i % 5 == 0)
        {
			totalCount++;
            count++;
            i /= 5;
        }
        return count;
    }

    public static int CountFactZeros(int num)
    {
        int count = 0;
        for (int i = 2; i <= num; i++)
        {
            count += FactorsOf5(i);
        }
        return count;
    }

    public static void Main(String[] args)
    {		
        for (int i = (int.MaxValue/100-10); i < (int.MaxValue/100); i++)
        {
			totalCount = 0;
            Console.WriteLine(i + " has " + CountFactZeros(i) + " zeros; Total operations = "+totalCount);
        }
    }
}
