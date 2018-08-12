<Query Kind="Program" />

class Program
{
    public class NameSet
    {
        private HashSet<String> names = new HashSet<String>();
        private int frequency = 0;
        private String rootName;

        public NameSet(String name, int freq)
        {
            names.Add(name);
            frequency = freq;
            rootName = name;
        }

        public HashSet<String> GetNames()
        {
            return names;
        }

        public String GetRootName()
        {
            return rootName;
        }

        public void CopyNamesWithFrequency(HashSet<String> more, int freq)
        {
            foreach (var item in more)
            {
                names.Add(item);
            }            
            frequency += freq;
        }

        public int GetFrequency()
        {
            return frequency;
        }

        public int Size()
        {
            return names.Count;
        }
    }

    /* Read through (name, frequency) pairs and initialize a mapping
     * of names to NameSets (equivalence classes).*/
    public static Dictionary<String, NameSet> ConstructGroups(Dictionary<String, int> names)
    {
        Dictionary<String, NameSet> groups = new Dictionary<String, NameSet>();
        foreach (var entry in names)
        {
            String name = entry.Key;
            int frequency = entry.Value;
            NameSet group = new NameSet(name, frequency);
            groups.Add(name, group);
        }
        return groups;
    }

    public static void MergeClasses(Dictionary<String, NameSet> groups, String[][] synonyms)
    {
        foreach (String[] entry in synonyms)
        {
            String name1 = entry[0];
            String name2 = entry[1];
            NameSet set1 = groups[name1];
            NameSet set2 = groups[name2];
            if (set1 != set2)
            {
                /* Always merge the smaller set into the bigger one. */
                NameSet smaller = set2.Size() < set1.Size() ? set2 : set1;
                NameSet bigger = set2.Size() < set1.Size() ? set1 : set2;

                /* Merge lists */
                HashSet<String> otherNames = smaller.GetNames();
                int frequency = smaller.GetFrequency();
                bigger.CopyNamesWithFrequency(otherNames, frequency);

                /* Update mapping */
                foreach (String name in otherNames)
                {
                    if (groups.ContainsKey(name))
                    {
                        groups[name] = bigger;
                    }
                    else
                    {
                        groups.Add(name, bigger);
                    }
                }
            }
        }
    }

    public static Dictionary<String, int> ConvertToMap(Dictionary<String, NameSet> groups)
    {
        Dictionary<String, int> list = new Dictionary<String, int>();
        foreach (NameSet group in groups.Values)
        {
            if (list.ContainsKey(group.GetRootName()))
            {
                list[group.GetRootName()] = group.GetFrequency();
            }
            else
            {
                list.Add(group.GetRootName(), group.GetFrequency());
            }
        }
        return list;
    }

    public static Dictionary<String, int> TrulyMostPopular(Dictionary<String, int> names, String[][] synonyms)
    {
        Dictionary<String, NameSet> groups = ConstructGroups(names);
        MergeClasses(groups, synonyms);
        return ConvertToMap(groups);
    }

    public static void Main(String[] args)
    {
        Dictionary<String, int> names = new Dictionary<String, int>();

        names.Add("John", 3);
        names.Add("Jonathan", 4);
        names.Add("Johnny", 5);
        names.Add("Chris", 1);
        names.Add("Kris", 3);
        names.Add("Brian", 2);
        names.Add("Bryan", 4);
        names.Add("Carleton", 4);

        String[][] synonyms = new string[][]
            {new string[] {"John", "Jonathan"},
             new string[] {"Jonathan", "Johnny"},
             new string[] {"Chris", "Kris"},
             new string[] {"Brian", "Bryan"}
            };

        Dictionary<String, int> finalList = TrulyMostPopular(names, synonyms);
        foreach (var entry in finalList)
        {
            String name = entry.Key;
            int frequency = entry.Value;
            Console.WriteLine(name + ": " + frequency);
        }

    }
}
