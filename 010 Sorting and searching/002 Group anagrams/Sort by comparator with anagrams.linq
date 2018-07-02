<Query Kind="Program" />

public static string StringArrayToString(string[] array)
{
    StringBuilder sb = new StringBuilder();
    foreach (string v in array)
    {
        sb.AppendFormat("{0}, ", v);
    }
    return sb.ToString();
}

private class AnagramComparator : IComparer
{
    private string SortChars(string s)
    {
        char[] content = s.ToCharArray();
        Array.Sort<char>(content);
        return new string(content);
    }

    int IComparer.Compare(Object x, Object y)
    {
        return SortChars((string)x).CompareTo(SortChars((string)y));
    }
}

public void Main()
{
    string[] array = { "apple", "banana", "carrot", "ele", "duck", "papel", "tarroc", "cudk", "eel", "lee" };
    Console.WriteLine(StringArrayToString(array));
    Array.Sort(array, new AnagramComparator());
    Console.WriteLine(StringArrayToString(array));
}
