<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

public class Program
{
    public class StackWithMin : Stack<int>
    {
        Stack<int> mins;

        public StackWithMin()
        {
            mins = new Stack<int>();
        }

        public new void Push(int value)
        {
            if (Min() == -1 ? true : value <= Min())
            {
                mins.Push(value);
            }
            base.Push(value);
        }

        public new int Pop()
        {
            int value = base.Pop();
            if (value == Min())
            {
                mins.Pop();
            }
            return value;
        }

        public int Min()
        {
            if (mins.Count == 0)
            {
                return -1;
            }
            else
            {
                return mins.Peek();
            }
        }
    }

    static void Main()
    {
        StackWithMin stack = new StackWithMin();
        int[] array = { 2, 1, 3, 1 };
        foreach (int value in array)
        {
            stack.Push(value);
            Console.Write(value + ", ");
        }
        Console.WriteLine('\n');
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine("Popped " + stack.Pop());
            Console.WriteLine("New min is " + stack.Min());
        }
    }
}
