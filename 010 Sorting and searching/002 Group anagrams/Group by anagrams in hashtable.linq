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

private string SortChars(string s)
{
    char[] content = s.ToCharArray();
    Array.Sort<char>(content);
    return new string(content);
}

private void Sort(string[] array)
{
    Dictionary<string, LinkedList<string>> hash = new Dictionary<string, LinkedList<string>>();

    /* Group words by anagram */
    foreach (string s in array)
    {
        string key = SortChars(s);
        if (!hash.ContainsKey(key))
        {
            hash.Add(key, new LinkedList<string>());
        }
        LinkedList<string> anagrams = hash[key];
        anagrams.AddLast(s);
    }

    /* Convert hash table to array */
    int index = 0;
    foreach (string key in hash.Keys)
    {
        LinkedList<string> list = hash[key];
        foreach (string t in list)
        {
            array[index] = t;
            index++;
        }
    }
}

public void Main()
{
    string[] array = { "apple", "banana", "carrot", "ele", "duck", "papel", "tarroc", "cudk", "eel", "lee" };
    Console.WriteLine(StringArrayToString(array));

    Sort(array);
    Console.WriteLine(StringArrayToString(array));
}
