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
        int[] births = GetSortedYears(people, true);
        int[] deaths = GetSortedYears(people, false);

        int birthIndex = 0;
        int deathIndex = 0;
        int currentlyAlive = 0;
        int maxAlive = 0;
        int maxAliveYear = min;

        /* Walk through arrays. */
        while (birthIndex < births.Length)
        {
            if (births[birthIndex] <= deaths[deathIndex])
            {
                currentlyAlive++; // include birth 
                if (currentlyAlive > maxAlive)
                {
                    maxAlive = currentlyAlive;
                    maxAliveYear = births[birthIndex];
                }
                birthIndex++; // move birth index
            }
            else if (births[birthIndex] > deaths[deathIndex])
            {
                currentlyAlive--; // include death
                deathIndex++; // move death index
            }
        }

        return maxAliveYear;
    }

    /* Copy birth years or death years (depending on the value of copyBirthYear)
     * into integer array, then sort array. */
    public static int[] GetSortedYears(Person[] people, Boolean copyBirthYear)
    {
        int[] years = new int[people.Length];
        for (int i = 0; i < people.Length; i++)
        {
            years[i] = copyBirthYear ? people[i].birth : people[i].death;
        }
        Array.Sort(years);
        return years;
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
