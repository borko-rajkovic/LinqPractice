<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class GraphPoint
    {
        public double x;
        public double y;
        public GraphPoint(double x1, double y1)
        {
            x = x1;
            y = y1;
        }

        public override String ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }

    public class Line
    {
        public static double epsilon = .5;
        public double slope;
        public double intercept;

        private Boolean infinite_slope = false;

        public Line(GraphPoint p, GraphPoint q)
        {
            if (Math.Abs(p.x - q.x) > epsilon)
            { // if x�s are different
                slope = (p.y - q.y) / (p.x - q.x); // compute slope
                intercept = p.y - slope * p.x; // y intercept from y=mx+b
            }
            else
            {
                infinite_slope = true;
                intercept = p.x; // x-intercept, since slope is infinite
            }
        }

        public Boolean IsEquivalent(double a, double b)
        {
            return (Math.Abs(a - b) < epsilon);
        }

        public void Print()
        {
            Console.WriteLine("y = " + slope + "x + " + intercept);
        }

        public static double FloorToNearestEpsilon(double d)
        {
            int r = (int)(d / epsilon);
            return ((double)r) * epsilon;
        }

        public Boolean IsEquivalent(Object o)
        {
            Line l = (Line)o;
            if (IsEquivalent(l.slope, slope) && IsEquivalent(l.intercept, intercept) && (infinite_slope == l.infinite_slope))
            {
                return true;
            }
            return false;
        }
    }

    public class HashMapList<T, E>
    {
        private Dictionary<T, List<E>> map = new Dictionary<T, List<E>>();

        /* Insert item into list at key. */
        public void Put(T key, E item)
        {
            if (!map.ContainsKey(key))
            {
                map.Add(key, new List<E>());
            }
            map[key].Add(item);
        }

        /* Insert list of items at key. */
        public void Put(T key, List<E> items)
        {
            map.Add(key, items);
        }

        /* Get list of items at key. */
        public List<E> Get(T key)
        {
            if (map.ContainsKey(key))
                return map[key];
            else
                return null;
        }

        /* Check if hashmaplist contains key. */
        public Boolean ContainsKey(T key)
        {
            return map.ContainsKey(key);
        }

        /* Check if list at key contains value. */
        public Boolean ContainsKeyValue(T key, E value)
        {
            List<E> list = Get(key);
            if (list == null) return false;
            return list.Contains(value);
        }

        /* Get the list of keys. */
        public Dictionary<T, List<E>>.KeyCollection KeySet()
        {
            return map.Keys;
        }

        public override String ToString()
        {
            return string.Join(", ", map);
        }
    }


    /* Find line that goes through most number of points. */
    public static Line FindBestLine(GraphPoint[] points)
    {
        HashMapList<Double, Line> linesBySlope = getListOfLines(points);
        return getBestLine(linesBySlope);
    }

    /* Add each pair of points as a line to the list. */
    public static HashMapList<Double, Line> getListOfLines(GraphPoint[] points)
    {
        HashMapList<Double, Line> linesBySlope = new HashMapList<Double, Line>();
        for (int i = 0; i < points.Length; i++)
        {
            for (int j = i + 1; j < points.Length; j++)
            {
                Line line = new Line(points[i], points[j]);
                double key = Line.FloorToNearestEpsilon(line.slope);
                linesBySlope.Put(key, line);
            }
        }
        return linesBySlope;
    }

    /* Return the line with the most equivalent other lines. */
    public static Line getBestLine(HashMapList<Double, Line> linesBySlope)
    {
        Line bestLine = null;
        int bestCount = 0;

        var slopes = linesBySlope.KeySet();

        foreach (double slope in slopes)
        {
            List<Line> lines = linesBySlope.Get(slope);
            foreach (Line line in lines)
            {
                /* count lines that are equivalent to current line */
                int count = countEquivalentLines(linesBySlope, line);

                /* if better than current line, replace it */
                if (count > bestCount)
                {
                    bestLine = line;
                    bestCount = count;
                    bestLine.Print();
                    Console.WriteLine(bestCount);
                }
            }
        }
        return bestLine;
    }

    /* Check Dictionary for lines that are equivalent. Note that we need to check one epsilon above and below the actual slope
     * since we're defining two lines as equivalent if they're within an epsilon of each other.
     */
    public static int countEquivalentLines(HashMapList<Double, Line> linesBySlope, Line line)
    {
        double key = Line.FloorToNearestEpsilon(line.slope);
        int count = countEquivalentLines(linesBySlope.Get(key), line);
        count += countEquivalentLines(linesBySlope.Get(key - Line.epsilon), line);
        count += countEquivalentLines(linesBySlope.Get(key + Line.epsilon), line);
        return count;
    }

    /* Count lines within an array of lines which are "equivalent" (slope and y-intercept are within an epsilon value) to a given line */
    public static int countEquivalentLines(List<Line> lines, Line line)
    {
        if (lines == null)
        {
            return 0;
        }

        int count = 0;
        foreach (Line parallelLine in lines)
        {
            if (parallelLine.IsEquivalent(line))
            {
                count++;
            }
        }
        return count;
    }

    public static GraphPoint[] CreatePoints()
    {
        int n_points = 100;
        Console.WriteLine("Points on Graph\n***************");
        GraphPoint[] points = new GraphPoint[n_points - 1];
        for (int i = 0; i < n_points / 2; i++)
        {
            GraphPoint p = new GraphPoint(i, 2.3 * ((double)i) + 20.0);
            points[i] = p;
            Console.WriteLine(p.ToString());
        }
        for (int i = 0; i < n_points / 2 - 1; i++)
        {
            GraphPoint p = new GraphPoint(i, 3.0 * ((double)i) + 1.0);
            points[n_points / 2 + i] = p;
            Console.WriteLine(p.ToString());
        }
        Console.WriteLine("****************\n");
        return points;
    }

    public static int Validate(Line line, GraphPoint[] points)
    {
        int count = 0;
        for (int i = 0; i < points.Length; i++)
        {
            for (int j = i + 1; j < points.Length; j++)
            {
                Line other = new Line(points[i], points[j]);
                if (line.IsEquivalent(other))
                {
                    count++;
                }
            }
        }
        return count;
    }

    public static void Main(String[] args)
    {
        GraphPoint[] points = CreatePoints();
        Line line = FindBestLine(points);
        line.Print();
        Console.WriteLine(Validate(line, points));

    }
}
