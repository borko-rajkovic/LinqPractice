<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static void Sort(Stack<int> s)
    {
        Stack<int> r = new Stack<int>();
        //while (!s.isEmpty())
        while (s.Count > 0)
        {
            /* Insert each element in s in sorted order into r. */
            int tmp = s.Pop();
            while (r.Count > 0 && r.Peek() > tmp)
            {
                s.Push(r.Pop());
            }
            r.Push(tmp);
        }

        /* Copy the elements back. */
        while (r.Count > 0)
        {
            s.Push(r.Pop());
        }
    }

    public static void Main(String[] args)
    {
        Stack<int> s = new Stack<int>();
        for (int i = 0; i < 10; i++)
        {
            int r = RandomIntInRange(0, 50);
            s.Push(r);
        }

        Sort(s);

        while (s.Count > 0)
        {
            Console.WriteLine(s.Pop());
        }
    }
}
