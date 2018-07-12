<Query Kind="Program" />

class Program
{
    public static int Search(String[] strings, String str, int first, int last)
    {
        if (first > last)
        {
            return -1;
        }

        /* Move mid to the middle */
        int mid = (last + first) / 2;

        /* If mid is empty, find closest non-empty string. */
        if (String.IsNullOrEmpty(strings[mid]))
        {
            int left = mid - 1;
            int right = mid + 1;
            while (true)
            {
                if (left < first && right > last)
                {
                    return -1;
                }
                else if (right <= last && !String.IsNullOrEmpty(strings[right]))
                {
                    mid = right;
                    break;
                }
                else if (left >= first && !String.IsNullOrEmpty(strings[left]))
                {
                    mid = left;
                    break;
                }
                right++;
                left--;
            }
        }

        /* Check for string, and recurse if necessary */
        if (str.Equals(strings[mid]))
        { // Found it!
            return mid;
        }
        else if (strings[mid].CompareTo(str) < 0)
        { // Search right
            return Search(strings, str, mid + 1, last);
        }
        else
        { // Search left
            return Search(strings, str, first, mid - 1);
        }
    }

    public static int Search(String[] strings, String str)
    {
        if (strings == null || str == null || String.IsNullOrEmpty(str))
        {
            return -1;
        }
        return Search(strings, str, 0, strings.Length - 1);
    }

    public static void Main(String[] args)
    {
        String[] stringList = { "apple", "", "", "banana", "", "", "", "carrot", "duck", "", "", "eel", "", "flower" };
        Console.WriteLine(Search(stringList, "ac"));

        foreach (String s in stringList)
        {
            String cloned = s;
            Console.WriteLine("<" + cloned + "> " + " appears at location " + Search(stringList, cloned));
        }
    }
}
