<Query Kind="Program" />

public class Program
{
    public class MyQueue<T>
    {
        Stack<T> stackNewest, stackOldest;

        public MyQueue()
        {
            stackNewest = new Stack<T>();
            stackOldest = new Stack<T>();
        }

        public int Size()
        {
            return stackNewest.Count + stackOldest.Count;
        }

        public void Add(T value)
        {
            // Push onto stack1
            stackNewest.Push(value);
        }

        /* Move elements from stackNewest into stackOldest. This is usually done so that we can
         * do operations on stackOldest.
         */
        private void ShiftStacks()
        {
            if (stackOldest.Count == 0)
            {
                while (!(stackNewest.Count == 0))
                {
                    stackOldest.Push(stackNewest.Pop());
                }
            }
        }

        public T Peek()
        {
            ShiftStacks();
            return stackOldest.Peek(); // retrieve the oldest item.
        }

        public T Remove()
        {
            ShiftStacks();
            return stackOldest.Pop(); // pop the oldest item.
        }
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int RandomInt(int n)
    {
        return RandomIntNumbers.Next(n);
    }

    private static readonly Random RandomIntNumbers = new Random();

    public static void Main()
    {

        MyQueue<int> my_queue = new MyQueue<int>();

        // Let's test our code against a "real" queue
        Queue<int> test_queue = new Queue<int>();

        for (int i = 0; i < 100; i++)
        {
			Console.WriteLine();
            int choice = RandomIntInRange(0, 10);
            if (choice <= 5)
            { // enqueue
                int element = RandomIntInRange(1, 10);
                test_queue.Enqueue(element);
                my_queue.Add(element);
                Console.WriteLine("Enqueued " + element);
            }
            else if (test_queue.Count > 0)
            {
                int top1 = test_queue.Dequeue();
                int top2 = my_queue.Remove();
                if (top1 != top2)
                { // Check for error
                    Console.WriteLine("******* FAILURE - DIFFERENT TOPS: " + top1 + ", " + top2);
                }
                Console.WriteLine("Dequeued " + top1);
            }

            if (test_queue.Count == my_queue.Size())
            {
                if (test_queue.Count > 0 && test_queue.Peek() != my_queue.Peek())
                {
                    Console.WriteLine("******* FAILURE - DIFFERENT TOPS: " + test_queue.Peek() + ", " + my_queue.Peek() + " ******");
                }
            }
            else
            {
                Console.WriteLine("******* FAILURE - DIFFERENT SIZES ******");
            }
        }

    }
}
