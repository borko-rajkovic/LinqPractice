<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class RankNode
    {
        public int left_size = 0;
        public RankNode left;
        public RankNode right;
        public int data = 0;
        public RankNode(int d)
        {
            data = d;
        }

        public void Insert(int d)
        {
            if (d <= data)
            {
                if (left != null)
                {
                    left.Insert(d);
                }
                else
                {
                    left = new RankNode(d);
                }
                left_size++;
            }
            else
            {
                if (right != null)
                {
                    right.Insert(d);
                }
                else
                {
                    right = new RankNode(d);
                }
            }
        }

        public int GetRank(int d)
        {
            if (d == data)
            {
                return left_size;
            }
            else if (d < data)
            {
                if (left == null)
                {
                    return -1;
                }
                else
                {
                    return left.GetRank(d);
                }
            }
            else
            {
                int right_rank = right == null ? -1 : right.GetRank(d);
                if (right_rank == -1)
                {
                    return -1;
                }
                else
                {
                    return left_size + 1 + right_rank;
                }
            }
        }
    }

    private static RankNode root = null;

    public static void Track(int number)
    {
        if (root == null)
        {
            root = new RankNode(number);
        }
        else
        {
            root.Insert(number);
        }
    }

    public static int GetRankOfNumber(int number)
    {
        return root.GetRank(number);
    }

    public static Random random = new Random();

    public static int RandomInt(int n)
    {
        return random.Next(n+1);
    }

    public static int RandomIntInRange(int min, int max)
    {
        return RandomInt(max + 1 - min) + min;
    }

    public static int[] RandomArray(int N, int min, int max)
    {
        int[] array = new int[N];
        for (int j = 0; j < N; j++)
        {
            array[j] = RandomIntInRange(min, max);
        }
        return array;
    }

    public static string ArrayToString(int[] array)
    {
        return string.Join(", ", array);
    }

    public static void Main(String[] args)
    {
        int size = 100;
        int[] list = RandomArray(size, -100, 100);
        for (int i = 0; i < list.Length; i++)
        {
            Track(list[i]);
        }

        int[] tracker = new int[size];
        for (int i = 0; i < list.Length; i++)
        {
            int v = list[i];
            int rank1 = root.GetRank(list[i]);
            tracker[rank1] = v;
        }

        for (int i = 0; i < tracker.Length - 1; i++)
        {
            if (tracker[i] != 0 && tracker[i + 1] != 0)
            {
                if (tracker[i] > tracker[i + 1])
                {
                    Console.WriteLine("ERROR at " + i);
                }
            }
        }

        Console.WriteLine("Array: " + ArrayToString(list));
        Console.WriteLine();
        Console.WriteLine("Ranks: " + ArrayToString(tracker));
    }

}
