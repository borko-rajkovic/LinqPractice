<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class LocationPair
    {
        public int location1;
        public int location2;
        public LocationPair(int first, int second)
        {
            SetLocations(first, second);
        }

        public void SetLocations(int first, int second)
        {
            this.location1 = first;
            this.location2 = second;
        }

        public void SetLocations(LocationPair loc)
        {
            SetLocations(loc.location1, loc.location2);
        }

        public int Distance()
        {
            return Math.Abs(location1 - location2);
        }

        public Boolean IsValid()
        {
            return location1 >= 0 && location2 >= 0;
        }

        public void UpdateWithMin(LocationPair loc)
        {
            if (!IsValid() || loc.Distance() < Distance())
            {
                SetLocations(loc);
            }
        }

        public override String ToString()
        {
            return "(" + location1 + ", " + location2 + ")";
        }
    }

    public static String GetLongTextBlob()
    {
        String book = "As they rounded a bend in the path that ran beside the river, Lara recognized the silhouette of a fig tree atop a nearby hill. The weather was hot and the days were long. The fig tree was in full leaf, but not yet bearing fruit. "
                + "Soon Lara spotted other landmarks�an outcropping of limestone beside the path that had a silhouette like a man�s face, a marshy spot beside the river where the waterfowl were easily startled, a tall tree that looked like a man with his arms upraised. They were drawing near to the place where there was an island in the river. The island was a good spot to make camp. They would sleep on the island tonight."
                + "Lara had been back and forth along the river path many times in her short life. Her people had not created the path�it had always been there, like the river�but their deerskin-shod feet and the wooden wheels of their handcarts kept the path well worn. Lara�s people were salt traders, and their livelihood took them on a continual journey. ";
        String book_mod = book.Replace('.', ' ').Replace(',', ' ').Replace('-', ' ');
        return book_mod;
    }

    public static String[] GetLongTextBlobAsStringList()
    {
        return GetLongTextBlob().Split(' ');
    }

    public static Dictionary<String, List<int>> GetWordLocations(String[] words)
    {
        Dictionary<String, List<int>> locations = new Dictionary<String, List<int>>();
        for (int i = 0; i < words.Length; i++)
        {
            if (!locations.ContainsKey(words[i]))
            {
                locations.Add(words[i], new List<int>());
            }
            locations[words[i]].Add(i);
        }
        return locations;
    }

    public static LocationPair FindMinDistancePair(List<int> array1, List<int> array2)
    {
        if (array1 == null || array2 == null || array1.Count == 0 || array2.Count == 0)
        {
            return null;
        }

        int index1 = 0;
        int index2 = 0;
        LocationPair best = new LocationPair(array1[0], array2[0]);
        LocationPair current = new LocationPair(array1[0], array2[0]);

        while (index1 < array1.Count && index2 < array2.Count)
        {
            current.SetLocations(array1[index1], array2[index2]);
            best.UpdateWithMin(current);
            if (current.location1 < current.location2)
            {
                index1++;
            }
            else
            {
                index2++;
            }
        }

        return best;
    }

    public static LocationPair FindClosest(String word1, String word2, Dictionary<String, List<int>> locations)
    {
        List<int> locations1 = locations[word1];
        List<int> locations2 = locations[word2];
        return FindMinDistancePair(locations1, locations2);
    }

    public static void Main(String[] args)
    {
        String[] wordlist = GetLongTextBlobAsStringList();
        String word1 = "river";
        String word2 = "life";
        Dictionary<String, List<int>> locations = GetWordLocations(wordlist);
        LocationPair pair = FindClosest(word1, word2, locations);
        Console.WriteLine("Distance between <" + word1 + "> and <" + word2 + ">: " + pair.ToString());
    }
}
