<Query Kind="Program" />

class Program
{
	private static Random random = new Random();
	public static char[] RandomString(int length)
	{
	    const string chars = "A1";
	    return Enumerable.Repeat(chars, length)
	      .Select(s => s[random.Next(s.Length)]).ToArray();
	}

    public static char[] ExtractSubarray(char[] array, int start, int end)
    {
        if (start > end) return null;
        char[] subarray = new char[end - start + 1];
        for (int i = start; i <= end; i++)
        {
            subarray[i - start] = array[i];
        }
        return subarray;
    }

    public static Boolean HasEqualLettersNumbers(char[] array, int start, int end)
    {
        int counter = 0;
        for (int i = start; i <= end; i++)
        {
            if (Char.IsLetter(array[i]))
            {
                counter++;
            }
            else if (Char.IsDigit(array[i]))
            {
                counter--;
            }
        }
        return counter == 0;
    }

    public static char[] FindLongestSubarray(char[] array)
    {
        for (int len = array.Length; len > 1; len--)
        {
            for (int i = 0; i <= array.Length - len; i++)
            {
                if (HasEqualLettersNumbers(array, i, i + len - 1))
                {
                    return ExtractSubarray(array, i, i + len - 1);
                }
            }
        }
        return null;
    }

    public static void Main(String[] args)
    {
        char b = '1';
        char a = 'a';
        char[] array = { a, b, a, b, a, b, b, b, b, b, a, a, a, a, a, b, a, b, a, b, b, a, a, a, a, a, a, a };
		array=RandomString(2000);
		
        for (int i = 0; i < array.Length; i++)
        {
            Console.Write(array[i] + " ");
        }
        Console.WriteLine();
        char[] max = FindLongestSubarray(array);
        Console.WriteLine(max.Length);
        for (int i = 0; i < max.Length; i++)
        {
            Console.Write(max[i] + " ");
        }
        Console.WriteLine("\nIs Valid? " + HasEqualLettersNumbers(max, 0, max.Length - 1));
    }
}
