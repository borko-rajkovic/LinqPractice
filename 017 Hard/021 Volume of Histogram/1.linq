<Query Kind="Program" />

class Program
{
    public static int FindIndexOfMax(int[] histogram, int start, int end)
    {
        int indexOfMax = start;
        for (int i = start + 1; i <= end; i++)
        {
            if (histogram[i] > histogram[indexOfMax])
            {
                indexOfMax = i;
            }
        }
        return indexOfMax;
    }

    public static int BorderedVolume(int[] histogram, int start, int end)
    {
        if (start >= end) return 0;

        int min = Math.Min(histogram[start], histogram[end]);
        int sum = 0;
        for (int i = start + 1; i < end; i++)
        {
            sum += min - histogram[i];
        }
        return sum;
    }

    public static int SubgraphVolume(int[] histogram, int start, int end, Boolean isLeft)
    {
        if (start >= end) return 0;
        int sum = 0;
        if (isLeft)
        {
            int max = FindIndexOfMax(histogram, start, end - 1);
            sum += BorderedVolume(histogram, max, end);
            sum += SubgraphVolume(histogram, start, max, isLeft);
        }
        else
        {
            int max = FindIndexOfMax(histogram, start + 1, end);
            sum += BorderedVolume(histogram, start, max);
            sum += SubgraphVolume(histogram, max, end, isLeft);
        }

        return sum;
    }

    public static int ComputeHistogramVolume(int[] histogram)
    {
        int start = 0;
        int end = histogram.Length - 1;

        int max = FindIndexOfMax(histogram, start, end);

        int leftVolume = SubgraphVolume(histogram, start, max, true);
        int rightVolume = SubgraphVolume(histogram, max, end, false);

        return leftVolume + rightVolume;
    }

    public static void Main(String[] args)
    {
        int[] histogram = { 0, 0, 4, 0, 0, 6, 0, 0, 3, 0, 8, 0, 2, 0, 5, 2, 0, 3, 0, 0 };
        int result = ComputeHistogramVolume(histogram);
        Console.WriteLine(result);
    }
}
