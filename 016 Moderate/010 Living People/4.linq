<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Threading.Tasks.dll</Reference>
</Query>

class Program
{
    public class Person
    {
        public int birth;
        public int death;
        public Person(int birthYear, int deathYear)
        {
            birth = birthYear;
            death = deathYear;
        }
    }

    public static int MaxAliveYear(Person[] people, int min, int max)
    {
        /* Build population delta array. */
        int[] populationDeltas = GetPopulationDeltas(people, min, max);
        int maxAliveYear = GetMaxAliveYear(populationDeltas);
        return maxAliveYear + min;
    }

    /* Add birth and death years to deltas array. */
    public static int[] GetPopulationDeltas(Person[] people, int min, int max)
    {
        int[] populationDeltas = new int[max - min + 2];
        foreach (Person person in people)
        {
            int birth = person.birth - min;
            populationDeltas[birth]++;

            int death = person.death - min;
            populationDeltas[death + 1]--;
        }
        return populationDeltas;
    }

    /* Compute running sums and return index with max. */
    public static int GetMaxAliveYear(int[] deltas)
    {
        int maxAliveYear = 0;
        int maxAlive = 0;
        int currentlyAlive = 0;
        for (int year = 0; year < deltas.Length; year++)
        {
            currentlyAlive += deltas[year];
            if (currentlyAlive > maxAlive)
            {
                maxAliveYear = year;
                maxAlive = currentlyAlive;
            }
        }

        return maxAliveYear;
    }

    public static void Main(String[] args)
    {
        int n = 3;
        int first = 1900;
        int last = 2000;
        Random random = new Random();
        Person[] people = new Person[n];
        for (int i = 0; i < n; i++)
        {
            int birth = first + random.Next(last - first);
            int death = birth + random.Next(last - birth);
            people[i] = new Person(birth, death);
            Console.WriteLine(birth + ", " + death);
        }
        int year = MaxAliveYear(people, first, last);
        Console.WriteLine(year);

    }
}
