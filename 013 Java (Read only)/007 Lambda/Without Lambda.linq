<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Country
    {
        private String name;
        private int size;
        private String continent;
        public Country(String n, String c, int sz)
        {
            name = n;
            size = sz;
            continent = c;
        }

        public String GetName()
        {
            return name;
        }

        public String GetContinent()
        {
            return continent;
        }

        public int GetPopulation()
        {
            return size;
        }
    }

    public static int getPopulation(List<Country> countries, String continent)
    {
        int sum = 0;
        foreach (Country c in countries)
        {
            if (c.GetContinent().Equals(continent))
            {
                sum += c.GetPopulation();
            }
        }
        return sum;
    }

    public static void Main(String[] args)
    {
        List<Country> countries = new List<Country>();
        countries.Add(new Country("United States", "North America", 1));
        countries.Add(new Country("Canada", "North America", 5));
        countries.Add(new Country("India", "Asia", 9));
        countries.Add(new Country("Japan", "Asia", 7));

        Console.WriteLine(getPopulation(countries, "Asia"));
    }
}
