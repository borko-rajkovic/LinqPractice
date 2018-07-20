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

    public static int GetPopulation(List<Country> countries, String continent)
    {
        //var sublist = countries.Where(country => country.GetContinent().Equals(continent));

        // Get list of populations
        //var populations = sublist.Select(c => c.GetPopulation());

        // One way of getting sum
        // int population = populations.Aggregate(0, (x, y) => x + y);

        // Sum aggregate - the other way
        // int population = populations.Sum();

        int population = countries.Select(c => c.GetContinent().Equals(continent) ? c.GetPopulation() : 0).Sum();

        return population;
    }

    public static void Main(String[] args)
    {
        List<Country> countries = new List<Country>();
        countries.Add(new Country("United States", "North America", 1));
        countries.Add(new Country("Canada", "North America", 5));
        countries.Add(new Country("India", "Asia", 9));
        countries.Add(new Country("Japan", "Asia", 7));

        Console.WriteLine(GetPopulation(countries, "Asia"));
    }
}
