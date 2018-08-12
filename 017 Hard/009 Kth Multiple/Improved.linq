<Query Kind="Program" />

class Program
{
    public static int RemoveMin(LinkedList<int> q)
    {
        int min = q.Last.Value;
        foreach (int v in q)
        {
            if (min > v)
            {
                min = v;
            }
        }
        while (q.Contains(min))
        {
            q.Remove(min);
        }
        return min;
    }

    public static void AddProducts(LinkedList<int> q, int v)
    {
        q.AddLast(v * 3);
        q.AddLast(v * 5);
        q.AddLast(v * 7);
    }

    public static int GetKthMagicNumber(int k)
    {
        if (k < 0)
        {
            return 0;
        }
        int val = 1;
        LinkedList<int> q = new LinkedList<int>();
        AddProducts(q, 1);
        for (int i = 0; i < k; i++)
        { // Start at 1 since we've already done one iteration
            val = RemoveMin(q);
            AddProducts(q, val);
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
