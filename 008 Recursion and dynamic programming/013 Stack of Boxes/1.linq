<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Box
    {
        public int width;
        public int height;
        public int depth;
        public Box(int w, int h, int d)
        {
            width = w;
            height = h;
            depth = d;
        }

        public Boolean CanBeUnder(Box b)
        {
            if (width > b.width && height > b.height && depth > b.depth)
            {
                return true;
            }
            return false;
        }

        public Boolean CanBeAbove(Box b)
        {
            if (b == null)
            {
                return true;
            }
            if (width < b.width && height < b.height && depth < b.depth)
            {
                return true;
            }
            return false;
        }

        public override String ToString()
        {
            return "Box(" + width + "," + height + "," + depth + ")";
        }
    }

    public class BoxComparator : IComparer<Box>
    {
        public int Compare(Box x, Box y)
        {
            return y.height - x.height;
        }
    }

    public static int CreateStack(List<Box> boxes)
    {
        boxes.Sort(new BoxComparator());
        int maxHeight = 0;
        for (int i = 0; i < boxes.Count; i++)
        {
            int height = CreateStack(boxes, i);
            maxHeight = Math.Max(maxHeight, height);
        }
        return maxHeight;
    }

    public static int CreateStack(List<Box> boxes, int bottomIndex)
    {
        Box bottom = boxes[bottomIndex];
        int maxHeight = 0;
        for (int i = bottomIndex + 1; i < boxes.Count; i++)
        {
            if (boxes[i].CanBeAbove(bottom))
            {
                int height = CreateStack(boxes, i);
                maxHeight = Math.Max(height, maxHeight);
            }
        }
        maxHeight += bottom.height;
        return maxHeight;
    }

    public static void Main(String[] args)
    {
        Box[] boxList = { new Box(6, 4, 4), new Box(8, 6, 2), new Box(5, 3, 3), new Box(7, 8, 3), new Box(4, 2, 2), new Box(9, 7, 3) };
        List<Box> boxes = new List<Box>();
        foreach (Box b in boxList)
        {
            boxes.Add(b);
        }

        int height = CreateStack(boxes);
        Console.WriteLine(height);
    }
}
