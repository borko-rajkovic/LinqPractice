<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{

    public class Point
    {
        public double x;
        public double y;
        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetLocation(double x, double y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Line
    {
        public double slope;
        public double yintercept;

        public Line(Point start, Point end)
        {
            double deltaY = end.y - start.y;
            double deltaX = end.x - start.x;
            slope = deltaY / deltaX; // Will be Infinity (not exception) when deltaX = 0 
            yintercept = end.y - slope * end.x;
        }

        public void Print()
        {
            Console.Write("y = " + slope + "x + " + yintercept);
        }
    }

    public static Point CreatePoint(int[] coordinates)
    {
        return new Point(coordinates[0], coordinates[1]);
    }

    /* Checks if middle is between start and end. */
    public static Boolean IsBetween(double start, double middle, double end)
    {
        if (start > end)
        {
            return end <= middle && middle <= start;
        }
        else
        {
            return start <= middle && middle <= end;
        }
    }

    /* Checks if middle is between start and end. */
    public static Boolean IsBetween(Point start, Point middle, Point end)
    {
        return IsBetween(start.x, middle.x, end.x) && IsBetween(start.y, middle.y, end.y);
    }

    public static void Swap(Point one, Point two)
    {
        double x = one.x;
        double y = one.y;
        one.SetLocation(two.x, two.y);
        two.SetLocation(x, y);
    }

    public static Point Intersection(Point start1, Point end1, Point start2, Point end2)
    {
        /* Rearranging these so that, in order of x values: start is before end and point 1 is before point 2. 
         * This will make some of the later logic simpler. */
        if (start1.x > end1.x) Swap(start1, end1);
        if (start2.x > end2.x) Swap(start2, end2);
        if (start1.x > start2.x)
        {
            Swap(start1, start2);
            Swap(end1, end2);
        }

        /* Compute lines (including slope and y-intercept). */
        Line line1 = new Line(start1, end1);
        Line line2 = new Line(start2, end2);

        /* If the lines are parallel, they intercept only if they have the same y intercept and start 2 is on line 1. */
        if (line1.slope == line2.slope)
        {
            if (line1.yintercept == line2.yintercept && IsBetween(start1, start2, end1))
            {
                return start2;
            }
            return null;
        }

        /* Get intersection coordinate. */
        double x = (line2.yintercept - line1.yintercept) / (line1.slope - line2.slope);
        double y = x * line1.slope + line1.yintercept;
        Point intersection = new Point(x, y);

        /* Check if within line segment range. */
        if (IsBetween(start1, intersection, end1) && IsBetween(start2, intersection, end2))
        {
            return intersection;
        }
        return null;
    }

    public static void Main(String[] args)
    {
        int[][] coordinates = {
            new int[] {1, 1}, new int[] {16, 16},
            new int[] {2, 1}, new int[] {1, 20}};
        Point[] points = { CreatePoint(coordinates[0]), CreatePoint(coordinates[1]), CreatePoint(coordinates[2]), CreatePoint(coordinates[3]) };
        Point intersection = Intersection(points[0], points[1], points[2], points[3]);
        if (intersection == null)
        {
            Console.WriteLine("No intersection.");
        }
        else
        {
            Console.WriteLine("Intersection: " + intersection.x + ", " + intersection.y);
        }
    }
}
