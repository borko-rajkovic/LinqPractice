<Query Kind="Program" />

public class Program
{
    public class Tower
    {
        private Stack<int> disks;
        private int index;

        public Tower(int i)
        {
            disks = new Stack<int>();
            index = i;
        }

        public int Index()
        {
            return index;
        }

        public void Add(int d)
        {
            if (!(disks.Count==0) && disks.Peek() <= d)
            {
                Console.WriteLine("Error placing disk " + d);
            }
            else
            {
                disks.Push(d);
            }
        }

        public void MoveTopTo(Tower t)
        {
            int top = disks.Pop();
            t.Add(top);
        }

        public void Print()
        {
            Console.WriteLine("Contents of Tower " + Index() + ": " + string.Join(",", disks));
        }

        public void MoveDisks(int n, Tower destination, Tower buffer)
        {
            if (n > 0)
            {
                String tag = "move_" + n + "_disks_from_" + this.index + "_to_" + destination.index + "_with_buffer_" + buffer.index;
                Console.WriteLine("<" + tag + ">");
                MoveDisks(n - 1, buffer, destination);
                Console.WriteLine("<move_top_from_" + this.index + "_to_" + destination.index + ">");
                Console.WriteLine("<before>");
                Console.WriteLine("<source_print>");
                this.Print();
                Console.WriteLine("</source_print>");
                Console.WriteLine("<destination_print>");
                destination.Print();
                Console.WriteLine("</destination_print>");
                Console.WriteLine("</before>");
                MoveTopTo(destination);
                Console.WriteLine("<after>");
                Console.WriteLine("<source_print>");
                Print();
                Console.WriteLine("</source_print>");
                Console.WriteLine("<destination_print>");
                destination.Print();
                Console.WriteLine("</destination_print>");
                Console.WriteLine("</after>");
                Console.WriteLine("</move_top_from_" + this.index + "_to_" + destination.index + ">");
                buffer.MoveDisks(n - 1, destination, this);
                Console.WriteLine("</" + tag + ">");
            }
        }
    }


    public static void Main(String[] args)
    {
        // Set up code.
        int n = 5;
        Tower[] towers = new Tower[3];
        for (int i = 0; i < 3; i++)
        {
            towers[i] = new Tower(i);
        }
        for (int i = n - 1; i >= 0; i--)
        {
            towers[0].Add(i);
        }

        // Copy and paste output into a .XML file and open it with internet explorer.
        towers[0].Print();
        towers[0].MoveDisks(n, towers[2], towers[1]);
        towers[2].Print();
    }

}
