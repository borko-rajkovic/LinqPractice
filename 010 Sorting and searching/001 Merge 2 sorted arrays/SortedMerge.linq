<Query Kind="Program" />

public static string ArrayToString(int[] array)
{
    StringBuilder sb = new StringBuilder();
    foreach (int v in array)
    {
        sb.AppendFormat("{0}, ", v);
    }
    return sb.ToString();
}

private static void Merge(int[] a, int[] b, int lastA, int lastB)
{
    int indexMerged = lastB + lastA - 1; /* Index of last location of merged array */
    int indexA = lastA - 1; /* Index of last element in array b */
    int indexB = lastB - 1; /* Index of last element in array a */

    /* Merge a and b, starting from the last element in each */
    while (indexB >= 0)
    {
        if (indexA >= 0 && a[indexA] > b[indexB])
        { /* end of a is bigger than end of b */
            a[indexMerged] = a[indexA]; // copy element
            indexA--;
        }
        else
        {
            a[indexMerged] = b[indexB]; // copy element
            indexB--;
        }
        indexMerged--; // move indices
    }
	
	//Elements of a are not copied, because they stay there from start (they do not need to be copied if they are lowest elements)
}

public static void Main(String[] args)
{
    int[] a = new int[] { 2, 3, 4, 5, 6, 8, 10, 100, 0, 0, 0, 0, 0, 0 };
    int[] b = new int[] { 1, 4, 5, 6, 7, 7 };
    Merge(a, b, 8, 6);
    Console.WriteLine(ArrayToString(a));
}
