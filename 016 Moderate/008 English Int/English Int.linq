<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static String[] smalls = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    public static String[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    public static String[] bigs = { "", "Thousand", "Million", "Billion" };
    public static String hundred = "Hundred";
    public static String negative = "Negative";

    public static String Convert(int num)
    {
        if (num == 0)
        {
            return smalls[0];
        }
        else if (num < 0)
        {
            return negative + " " + Convert(-1 * num);
        }

        LinkedList<String> parts = new LinkedList<String>();
        int chunkCount = 0;

        while (num > 0)
        {
            if (num % 1000 != 0)
            {
                String chunk = ConvertChunk(num % 1000) + " " + bigs[chunkCount];
                parts.AddFirst(chunk);
            }
            num /= 1000; // shift chunk
            chunkCount++;
        }

        return ListToString(parts);
    }

    /* Convert a linked list of strings to a string, dividing it up with spaces. */
    public static String ListToString(LinkedList<String> parts)
    {
        StringBuilder sb = new StringBuilder();
        while (parts.Count > 1)
        {
            sb.Append(parts.First.Value);
            parts.RemoveFirst();
            sb.Append(" ");
        }
        sb.Append(parts.First.Value);
        parts.RemoveFirst();
        return sb.ToString();
    }

    public static String ConvertChunk(int number)
    {
        LinkedList<String> parts = new LinkedList<String>();

        /* Convert hundreds place */
        if (number >= 100)
        {
            parts.AddLast(smalls[number / 100]);
            parts.AddLast(hundred);
            number %= 100;
        }

        /* Convert tens place */
        if (number >= 10 && number <= 19)
        {
            parts.AddLast(smalls[number]);
        }
        else if (number >= 20)
        {
            parts.AddLast(tens[number / 10]);
            number %= 10;
        }

        /* Convert ones place */
        if (number >= 1 && number <= 9)
        {
            parts.AddLast(smalls[number]);
        }

        return ListToString(parts);
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static void Main(String[] args)
    {
        /* numbers between 100000 and 1000000 */
        for (int i = 0; i < 8; i++)
        {
            int value = (int)(-1 * Math.Pow(10, i));
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }

        /* numbers between 0 and 100 */
        for (int i = 0; i < 10; i++)
        {
            int value = RandomIntInRange(0, 100);
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }

        /* numbers between 100 and 1000 */
        for (int i = 0; i < 10; i++)
        {
            int value = RandomIntInRange(100, 1000);
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }

        /* numbers between 1000 and 100000 */
        for (int i = 0; i < 10; i++)
        {
            int value = RandomIntInRange(1000, 100000);
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }


        /* numbers between 100000 and 100000000 */
        for (int i = 0; i < 10; i++)
        {
            int value = RandomIntInRange(100000, 100000000);
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }

        /* numbers between 100000000 and 1000000000 */
        for (int i = 0; i < 10; i++)
        {
            int value = RandomIntInRange(100000000, 1000000000);
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }

        /* numbers between 1000000000 and int.MAX_VALUE */
        for (int i = 0; i < 10; i++)
        {
            int value = RandomIntInRange(1000000000, int.MaxValue);
            String s = Convert(value);
            Console.WriteLine(value + ": " + s);
        }
    }
}
