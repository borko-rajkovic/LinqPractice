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

    public static LocationPair FindClosest(String[] words, String word1, String word2)
    {
        LocationPair best = new LocationPair(-1, -1);
        LocationPair current = new LocationPair(-1, -1);
        for (int i = 0; i < words.Length; i++)
        {
            String word = words[i];
            if (word.Equals(word1))
            {
                current.location1 = i;
                best.UpdateWithMin(current);
            }
            else if (word.Equals(word2))
            {
                current.location2 = i;
                best.UpdateWithMin(current);
            }
        }
        return best;
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

    public static void Main(String[] args)
    {
        String[] wordlist = GetLongTextBlobAsStringList();
        String word1 = "river";
        String word2 = "life";
        LocationPair pair = FindClosest(wordlist, word1, word2);
        Console.WriteLine("Distance between <" + word1 + "> and <" + word2 + ">: " + pair.ToString());
    }
}
