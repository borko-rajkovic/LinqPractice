<Query Kind="Program" />

class Program
{
    public static void PrintQueue(LinkedList<int> q, int x)
    {
        Console.Write(x + ": ");
        foreach (int a in q)
        {
            Console.Write(a / x + ", ");
        }
        Console.WriteLine("");
    }

    public static int GetKthMagicNumber(int k)
    {
        if (k < 0)
        {
            return 0;
        }
        int val = 0;
        LinkedList<int> queue3 = new LinkedList<int>();
        LinkedList<int> queue5 = new LinkedList<int>();
        LinkedList<int> queue7 = new LinkedList<int>();
        queue3.AddLast(1);
        for (int i = 0; i <= k; i++)
        { // Include 0th iteration through kth iteration
            int v3 = queue3.Count > 0 ? queue3.First.Value : int.MaxValue;
            int v5 = queue5.Count > 0 ? queue5.First.Value : int.MaxValue;
            int v7 = queue7.Count > 0 ? queue7.First.Value : int.MaxValue;
            val = Math.Min(v3, Math.Min(v5, v7));
            if (val == v3)
            {
                queue3.Remove(val);
                queue3.AddLast(3 * val);
                queue5.AddLast(5 * val);
            }
            else if (val == v5)
            {
                queue5.Remove(val);
                queue5.AddLast(5 * val);
            }
            else if (val == v7)
            {
                queue7.Remove(val);
            }
            queue7.AddLast(7 * val);
        }
        return val;
    }

    public static void Main(String[] args)
    {
        for (int i = 0; i < 50; i++)
        {
            Console.WriteLine(i + " : " + GetKthMagicNumber(i));
        }
    }
}
