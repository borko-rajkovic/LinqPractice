<Query Kind="Program" />

public class Program
{
    public class Node
    {
        public Node above;
        public Node below;
        public int value;
        public Node(int value)
        {
            this.value = value;
        }
    }

    public class Stack
    {
        private int capacity;
        public Node top;
        public Node bottom;
        public int size = 0;

        public Stack(int capacity)
        {
            this.capacity = capacity;
        }

        public Boolean IsFull()
        {
            return capacity == size;
        }

        public void Join(Node above, Node below)
        {
            if (below != null) below.above = above;
            if (above != null) above.below = below;
        }

        public Boolean Push(int v)
        {
            if (size >= capacity) return false;
            size++;
            Node n = new Node(v);
            if (size == 1) bottom = n;
            Join(n, top);
            top = n;
            return true;
        }

        public int Pop()
        {
            if (top == null) throw new Exception("Stack is empty");
            Node t = top;
            top = top.below;
            size--;
            return t.value;
        }

        public Boolean IsEmpty()
        {
            return size == 0;
        }

        public int RemoveBottom()
        {
            Node b = bottom;
            bottom = bottom.above;
            if (bottom != null) bottom.below = null;
            size--;
            return b.value;
        }
    }

    public class SetOfStacks
    {
        List<Stack> stacks = new List<Stack>();
        public int capacity;

        public SetOfStacks(int capacity)
        {
            this.capacity = capacity;
        }

        public Stack GetLastStack()
        {
            if (stacks.Count == 0)
            {
                return null;
            }
            return stacks[stacks.Count - 1];
        }

        public void Push(int v)
        {
            Stack last = GetLastStack();
            if (last != null && !last.IsFull())
            { // add to last
                last.Push(v);
            }
            else
            { // must create new stack
                Stack stack = new Stack(capacity);
                stack.Push(v);
                stacks.Add(stack);
            }
        }

        public int Pop()
        {
            Stack last = GetLastStack();
            if (last == null) throw new Exception("Stack is empty");
            int v = last.Pop();
            if (last.size == 0)
            {
                stacks.RemoveAt(stacks.Count - 1);
            }
            return v;
        }

        public int PopAt(int index)
        {
            return LeftShift(index, true);
        }

        public int LeftShift(int index, Boolean removeTop)
        {
            Stack stack = stacks[index];
            int removed_item;
            if (removeTop) removed_item = stack.Pop();
            else removed_item = stack.RemoveBottom();
            if (stack.IsEmpty())
            {
                stacks.RemoveAt(index);
            }
            else if (stacks.Count > index + 1)
            {
                int v = LeftShift(index + 1, false);
                stack.Push(v);
            }
            return removed_item;
        }

        public Boolean IsEmpty()
        {
            Stack last = GetLastStack();
            return last == null || last.IsEmpty();
        }
    }

    public static void Main(String[] args)
    {
        int capacity_per_substack = 5;
        SetOfStacks set = new SetOfStacks(capacity_per_substack);
        for (int i = 0; i < 34; i++)
        {
            set.Push(i);
        }
        int test = set.PopAt(4);

        Console.WriteLine(test);

        for (int i = 0; i < 33; i++)
        {
            Console.WriteLine("Popped " + set.Pop());
        }

    }
}
