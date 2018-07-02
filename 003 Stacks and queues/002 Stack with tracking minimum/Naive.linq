<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

public class Program
{
    public class NodeWithMin
    {
        public int value;
        public int min;
        public NodeWithMin(int v, int min)
        {
            value = v;
            this.min = min;
        }
    }

    public class StackWithMin : Stack<NodeWithMin>
    {
        public void Push(int value)
        {
            int newMin = Min() == -1 ? value : Math.Min(value, Min());
            Push(new NodeWithMin(value, newMin));
        }

        public int Min()
        {
            if (this.Count == 0)
            {
                return -1;
            }
            else
            {
                return Peek().min;
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
            Console.WriteLine("Popped " + stack.Pop().value);
            Console.WriteLine("New min is " + stack.Min());
        }
    }
}
