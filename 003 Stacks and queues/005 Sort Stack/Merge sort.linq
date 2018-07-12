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

    public static Stack<int> Mergesort(Stack<int> inStack)
    {
        if (inStack.Count <= 1)
        {
            return inStack;
        }

        Stack<int> left = new Stack<int>();
        Stack<int> right = new Stack<int>();
        int count = 0;
        while (inStack.Count != 0)
        {
            count++;
            if (count % 2 == 0)
            {
                left.Push(inStack.Pop());
            }
            else
            {
                right.Push(inStack.Pop());
            }
        }

        left = Mergesort(left);
        right = Mergesort(right);

        while (left.Count > 0 || right.Count > 0)
        {
            if (left.Count == 0)
            {
                inStack.Push(right.Pop());
            }
            else if (right.Count == 0)
            {
                inStack.Push(left.Pop());
            }
            else if (right.Peek().CompareTo(left.Peek()) <= 0)
            {
                inStack.Push(left.Pop());
            }
            else
            {
                inStack.Push(right.Pop());
            }
        }

        Stack<int> reverseStack = new Stack<int>();
        while (inStack.Count > 0)
        {
            reverseStack.Push(inStack.Pop());
        }

        return reverseStack;
    }

    public static void Main(String[] args)
    {
        Stack<int> s = new Stack<int>();
        for (int i = 0; i < 10; i++)
        {
            int r = RandomIntInRange(0, 50);
            s.Push(r);
        }

        Stack<int> reverseStack = Mergesort(s);

        while (reverseStack.Count > 0)
        {
            s.Push(reverseStack.Pop());
        }

        while (s.Count > 0)
        {
            Console.WriteLine(s.Pop());
        }
    }
}
